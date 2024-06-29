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
    Task<string> SendResetPasswordCode(string email);
    Task<string> ConfirmResetPassword(string resetCode, string email);
    Task<string> ResetPassword(string email, string password);
}
