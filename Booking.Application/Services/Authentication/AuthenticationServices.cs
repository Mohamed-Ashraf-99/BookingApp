﻿using Booking.Application.Authentication.Helpers;
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
namespace SchoolProject.Service.Implementations;

public class AuthenticationServices : IAuthenticationServices
{
    #region Fields
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly UserManager<User> _userManager;
    #endregion 

    #region Constructors
    public AuthenticationServices(JwtSettings jwtSettings,
                                 IRefreshTokenRepository refreshTokenRepository,
                                 UserManager<User> userManager
                          )
    {
        _jwtSettings = jwtSettings;
        _refreshTokenRepository = refreshTokenRepository;
        _userManager = userManager;
    }


    #endregion

    #region Handle Functions

    public async Task<JwtAuthResult> GetJWTToken(User user)
    {
        var (jwtToken, accessToken) = await GenerateJWTToken(user);
        var refreshToken = GetRefreshToken(user.UserName);
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

    private RefreshToken GetRefreshToken(string username)
    {
        var refreshToken = new RefreshToken
        {
            ExpireDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
            UserName = username,
            Token = GenerateRefreshToken()
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
            return "ErrorWhenConfirmEmail";
        return "Success";
    }


    #endregion
}