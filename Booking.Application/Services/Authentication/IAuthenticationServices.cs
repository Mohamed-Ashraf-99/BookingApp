using Booking.Application.Authentication.Helpers;
using Booking.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Booking.Application.Services.Authentication;

public interface IAuthenticationServices
{
    public Task<JwtAuthResult> GetJWTToken(User user);
    public JwtSecurityToken ReadJWTToken(string accessToken);
    public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
    public Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
    public Task<string> ValidateToken(string AccessToken);
   
}
