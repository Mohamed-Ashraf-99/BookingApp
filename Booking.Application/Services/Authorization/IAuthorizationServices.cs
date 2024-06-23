namespace Booking.Application.Services.Authorization;

public interface IAuthorizationServices
{
    Task<string> AddRoleAsync(string roleName);
    Task<bool> IsRoleExists(string roleName);
}
