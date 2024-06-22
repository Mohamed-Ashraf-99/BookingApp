using Booking.Domain.Entities.Identity;

namespace Booking.Application.Services.Authentication;

public interface IAuthenticationServices
{
    Task<string> GetJWTToken(User user);
}
