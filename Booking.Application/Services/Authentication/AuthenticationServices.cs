using Booking.Application.Authentication.Helpers;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Booking.Application.Services.Authentication;

public class AuthenticationServices(JwtSettings _jwtSettings,
    IRefreshTokenRepository _refreshTokenRepository,
    UserManager<User> _userManager) : IAuthenticationServices
{
    public async Task<JwtAuthResult> GetJWTToken(User user)
    {
        var token = GenerateJwtToken(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var accessToken = tokenHandler.WriteToken(token);

        var refreshToken = GetRefreshToken(user);

        var userRefreshToken = new UserRefreshToken()
        {
            CreationDate = DateTime.Now,
            ExpireDate = DateTime.Now.AddDays(30),
            IsUsed = true,
            IsRevoked = false,
            JwtId = token.Id,
            RefreshToken = refreshToken.Token,
            Token = accessToken,
            UserId = user.Id,
        };

        await _refreshTokenRepository.AddAsync(userRefreshToken);

        return new JwtAuthResult
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
    }

    private JwtSecurityToken GenerateJwtToken(User user)
    {
        var claims = GetClaims(user);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: _jwtSettings.Issuer, audience: _jwtSettings.Audience, claims: claims, expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: credentials);

        return token;
    }

    private RefreshToken GetRefreshToken(User user)
    {
        var refreshToken = new RefreshToken()
        {
            UserName = user.UserName,
            Token = GenerateRefreshToken(),
            ExpireDate = DateTime.Now.AddDays(30),
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

    private List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(nameof(UserClaimModel.UserName), user.UserName),
            new Claim(nameof(UserClaimModel.Email), user.Email),
            new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
            new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
        };
        return claims;
    }

    public async Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken)
    {
        //Read Token to get claims
        var token = ReadJwtToken(accessToken);
        if (token is null || token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
        {
            throw new SecurityTokenException();
        }
        if (token.ValidTo > DateTime.UtcNow)
        {
            throw new SecurityTokenException("Token is not expired");
        }

        //Get User
        var userId = token?.Claims?.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;

        var userRefreshToken = await _refreshTokenRepository
            .GetTableNoTracking().FirstOrDefaultAsync(x
            => x.Token == accessToken &&
               x.RefreshToken == refreshToken &&
               x.UserId == int.Parse(userId)
            );

        if (userRefreshToken is null)
            throw new SecurityTokenException("Refresh token is not found");

        if (userRefreshToken.ExpireDate < DateTime.UtcNow)
        {
            userRefreshToken.IsRevoked = true;
            userRefreshToken.IsUsed = false;
            await _refreshTokenRepository.UpdateAsync(userRefreshToken);   
            throw new SecurityTokenException("Refresh Token is expired");
        }


        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            throw new SecurityTokenException("User is not found");

        var response = GenerateJwtToken(user);

        var refreshTokenResult = new RefreshToken()
        {
            UserName = token?.Claims?.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value,
            Token = refreshToken,
            ExpireDate = userRefreshToken.ExpireDate,
        };

        return new JwtAuthResult
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenResult,
        };
    }

    private JwtSecurityToken ReadJwtToken(string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
            throw new ArgumentNullException(nameof(accessToken));

        var handler = new JwtSecurityTokenHandler();
        var response = handler.ReadJwtToken(accessToken);
        return response;
    }

    public async Task<string> ValidateToken(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var response = handler.ReadJwtToken(accessToken);

        var parameter = new TokenValidationParameters
        {
            ValidateIssuer = _jwtSettings.ValidateIssuer,
            ValidIssuers = new[] { _jwtSettings.Issuer },
            ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSignInKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            ValidAudience = _jwtSettings.Audience,
            ValidateAudience = _jwtSettings.ValidateAudience,
            ValidateLifetime = _jwtSettings.ValidateLifeTime,
        };
        var validatorToken = handler.ValidateToken(accessToken, parameter, out SecurityToken validatedToken);
        try
        {
            if (validatorToken is null)
                throw new SecurityTokenException("Invalid Token");

            return "Succeeded";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
