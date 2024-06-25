using Booking.Domain.Entities.Identity;

namespace Booking.Application.Services.CurrentUser;

public interface ICurrentUserService
{
    Task<User> GetUserAsync();
    Task<int> GetUserId();
    Task<List<string>> GetCurrentUserRolesAsync();
}
