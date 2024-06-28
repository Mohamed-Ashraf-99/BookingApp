
using Booking.Domain.Entities.Identity;

namespace Booking.Application.Services.ApplicationUser;

public interface IApplicationUserService
{
    Task<string> CreateUserAsync(User user, string password);
}
