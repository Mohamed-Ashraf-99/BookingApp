using Booking.Application.Authentication.Helpers;
using Booking.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Booking.Application.Services.Authentication;

public interface IAuthenticationServices
{
    Task<JwtAuthResult> GetJWTToken(User user);
    JwtSecurityToken ReadJWTToken(string accessToken);
    Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
    Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
    Task<string> ValidateToken(string AccessToken);
    Task<string> ConfirmEmail(int? userId, string code);
}
