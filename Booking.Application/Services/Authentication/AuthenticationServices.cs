using Booking.Application.Authentication.Helpers;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Booking.Application.Services.Authentication;

public class AuthenticationServices(JwtSettings _jwtSettings,
    IRefreshTokenRepository _refreshTokenRepository) : IAuthenticationServices
{
    private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
    public async Task<JwtAuthResult> GetJWTToken(User user)
    {
        var claims = GetClaims(user);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer: _jwtSettings.Issuer, audience: _jwtSettings.Audience, claims: claims, expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();

        var accessToken = tokenHandler.WriteToken(token);

        var refreshToken = GetRefreshToken(user);

        var userRefreshToken = new UserRefreshToken()
        {
            CreationDate = DateTime.Now,
            ExpireDate = DateTime.Now.AddDays(30),
            IsUsed = false,
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

    private RefreshToken GetRefreshToken(User user)
    {
        var refreshToken = new RefreshToken()
        {
            UserName = user.UserName,
            Token = GenerateRefreshToken(),
            ExpireDate = DateTime.Now.AddDays(30),
        };
        _userRefreshToken.AddOrUpdate(refreshToken.Token, refreshToken, (s, t) => refreshToken);
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
        };
        return claims;
    }
}
