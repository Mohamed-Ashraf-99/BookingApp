using Booking.Application.Authentication.Helpers;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Booking.Application.Services.Authentication;
using Booking.Application.Services.CurrentUser;
using Booking.Domain.Exceptions;
using Booking.Application.Services.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Booking.Infrastructure.Persistence;
namespace SchoolProject.Service.Implementations;

public class AuthenticationServices : IAuthenticationServices
{
    #region Fields
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly ILogger<AuthenticationServices> _logger;
    private readonly BookingDbContext _context;

    #endregion

    #region Constructors
    public AuthenticationServices(JwtSettings jwtSettings,
                                 IRefreshTokenRepository refreshTokenRepository,
                                 UserManager<User> userManager,
                                 IEmailService emailService,
                                 ILogger<AuthenticationServices> logger,
                                 BookingDbContext context
                          )
    {
        _jwtSettings = jwtSettings;
        _refreshTokenRepository = refreshTokenRepository;
        _userManager = userManager;
        _emailService = emailService;
        _logger = logger;
        _context = context;
    }


    #endregion

    #region Handle Functions

    public async Task<JwtAuthResult> GetJWTToken(User user)
    {
        var (jwtToken, accessToken) = await GenerateJWTToken(user);
        var userRoles = await GetUserRoles(user);
        var refreshToken = await GetRefreshToken(user.UserName, user.Id, userRoles);
        var userRefreshToken = new UserRefreshToken
        {
            CreationDate = DateTime.Now,
            ExpireDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
            IsUsed = true,
            IsRevoked = false,
            JwtId = jwtToken.Id,
            RefreshToken = refreshToken.Token,
            Token = accessToken,
            UserId = user.Id
        };
        await _refreshTokenRepository.AddAsync(userRefreshToken);

        var response = new JwtAuthResult();
        response.RefreshToken = refreshToken;
        response.AccessToken = accessToken;
        return response;
    }

    private async Task<List<string>> GetUserRoles(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return roles.ToList();
    }

    private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
    {
        var claims = await GetClaims(user);
        var jwtToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return (jwtToken, accessToken);
    }

    public async Task<List<Claim>> GetClaims(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.NameIdentifier,user.UserName),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
            new Claim(nameof(UserClaimModel.Id), user.Id.ToString())
        };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);
        return claims;
    }

    private async Task<RefreshToken> GetRefreshToken(string username, int userId, List<string> userRoles)
    {

        var refreshToken = new RefreshToken
        {
            ExpireDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
            UserName = username,
            Token = GenerateRefreshToken(),
            UserId = userId,
            UserRoles = userRoles
        };
        return refreshToken;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        var randomNumberGenerate = RandomNumberGenerator.Create();
        randomNumberGenerate.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
    {
        var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
        var response = new JwtAuthResult();
        response.AccessToken = newToken;
        var refreshTokenResult = new RefreshToken();
        refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
        refreshTokenResult.Token = refreshToken;
        refreshTokenResult.ExpireDate = (DateTime)expiryDate;
        response.RefreshToken = refreshTokenResult;
        return response;

    }
    public JwtSecurityToken ReadJWTToken(string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new ArgumentNullException(nameof(accessToken));
        }
        var handler = new JwtSecurityTokenHandler();
        var response = handler.ReadJwtToken(accessToken);
        return response;
    }

    public async Task<string> ValidateToken(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = _jwtSettings.ValidateIssuer,
            ValidIssuers = new[] { _jwtSettings.Issuer },
            ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
            ValidAudience = _jwtSettings.Audience,
            ValidateAudience = _jwtSettings.ValidateAudience,
            ValidateLifetime = _jwtSettings.ValidateLifeTime,
        };
        try
        {
            var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

            if (validator == null)
            {
                return "InvalidToken";
            }

            return "NotExpired";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
    {
        
        if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
        {
            return ("AlgorithmIsWrong", null);
        }
        if (jwtToken.ValidTo > DateTime.UtcNow)
        {
            return ("TokenIsNotExpired", null);
        }

        //Get User

        var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
        var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                                         .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                 x.RefreshToken == refreshToken &&
                                                                 x.UserId == int.Parse(userId));
        if (userRefreshToken == null)
        {
            return ("RefreshTokenIsNotFound", null);
        }

        if (userRefreshToken.ExpireDate < DateTime.UtcNow)
        {
            userRefreshToken.IsRevoked = true;
            userRefreshToken.IsUsed = false;
            await _refreshTokenRepository.UpdateAsync(userRefreshToken);
            return ("RefreshTokenIsExpired", null);
        }
        var expirydate = userRefreshToken.ExpireDate;
        return (userId, expirydate);
    }

    public async Task<string> ConfirmEmail(int? userId, string code)
    {
        if (userId == null || code is null)
            return "ErrorWhenConfirmEmail";
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
        if (!confirmEmail.Succeeded)
            return "Error When Confirm Email";
        return "Success";
    }

    #region Reset Password
    public async Task<string> SendResetPasswordCode(string email)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // 1. Get the user
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return $"There are no users with Email: {email}";
            }

            // 2. Generate the reset code
            var resetCode = GenerateRandomCodeNumber();

            // 3. Update the user's code
            user.Code = resetCode;
            var identityResult = await _userManager.UpdateAsync(user);
            if (!identityResult.Succeeded)
            {
                return "Error while updating User table";
            }

            // 4. Send the reset code via email
            var message = await SendPasswordResetEmail(user, resetCode);

            if (message is not null)
            {
                await transaction.CommitAsync();
                return message;
            }
            else
            {
                return "Failed to send password reset email.";
            }
        }
        catch (Exception ex)
        {
             await transaction.RollbackAsync();
            _logger.LogError(ex, "An error occurred in SendResetPasswordCode method for email: {Email}", email);

            return "An error occurred while processing your request. Please try again later.";
        }
    }

    private async Task<string> SendPasswordResetEmail(User user, string resetCode)
    {
        var filePath = $"{Directory.GetCurrentDirectory()}\\Template\\PasswordResetEmailTemplate.html";
        var str = new StreamReader(filePath);
        var mailText = str.ReadToEnd();
        str.Close();

        // Replace placeholders with actual user data and reset code
        mailText = mailText.Replace("[User's Name]", user.UserName)
                           .Replace("[Reset Code]", resetCode)
                           .Replace("[YourAppName]", "Tourist App");

        await _emailService.SendEmailAsync(user.Email, mailText, "Password Reset Request");

        return "Password reset email sent successfully.";
    }

    private string GenerateRandomCodeNumber()
    {
        var random = new Random();
        string code = random.Next(0, 1000000).ToString("D6");
        return code;
    }

    public async Task<string> ConfirmResetPassword(string resetCode, string email)
    {
        try
        {
            // 1->> Get User by email
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                _logger.LogWarning($"User with email '{email}' not found.");
                return "Failed: User not found.";
            }

            // 2->> Decrypt the code from the database
            var storedCode = user.Code;

            // 3->> Check equality 
            if (resetCode.Equals(storedCode))
            {
                return "Succeeded";
            }
            else
            {
                return "Failed: Invalid reset code.";
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An error occurred in ResetPassword method.");
            return "Failed: An error occurred while resetting the password. Please try again later.";
        }
    }

    public async Task<string> ResetPassword(string email, string password)
    {
        var transaction = _context.Database.BeginTransaction();

        try
        {
            // 1. Find the user by email
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogWarning($"User with email '{email}' not found.");
                return "Failed: User not found.";
            }

            // 2. Remove existing password (if any) and set new password
            var removeResult = await _userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
            {
                var errorMessage = string.Join(", ", removeResult.Errors.Select(e => e.Description));
                _logger.LogError($"Failed to remove existing password for user '{user.Email}': {errorMessage}");
                return "Failed: Unable to reset password. Please try again later.";
            }

            var addResult = await _userManager.AddPasswordAsync(user, password);
            if (!addResult.Succeeded)
            {
                var errorMessage = string.Join(", ", addResult.Errors.Select(e => e.Description));
                _logger.LogError($"Failed to set new password for user '{user.Email}': {errorMessage}");
                return "Failed: Unable to reset password. Please try again later.";
            }

            // 3. Commit transaction if everything succeeds
            await transaction.CommitAsync();
            return "Password reset successfully.";
        }
        catch (Exception ex)
        {
            // 4. Rollback transaction on exception
            await transaction.RollbackAsync();
            _logger.LogError(ex, $"An error occurred while resetting password for user '{email}'.");
            return "Failed: An unexpected error occurred. Please try again later.";
        }
    }



    #endregion

    #endregion
}