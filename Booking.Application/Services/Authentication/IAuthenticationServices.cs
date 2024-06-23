using Booking.Application.Authentication.Helpers;
using Booking.Domain.Entities.Identity;

namespace Booking.Application.Services.Authentication;

public interface IAuthenticationServices
{
    Task<JwtAuthResult> GetJWTToken(User user);
    Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken);
    Task<string> ValidateToken(string accessToken);
}
