
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Services.Authorization;

public class AuthorizationServices(RoleManager<IdentityRole<int>> _roleManager) : IAuthorizationServices
{
    public async Task<string> AddRoleAsync(string roleName)
    {
        var identityRole = new IdentityRole<int>();
        identityRole.Name = roleName;
        var role = await _roleManager.CreateAsync(identityRole);
        if (role.Succeeded)
        {
            return "Succeeded";
        }
        return "Failed";
    }

    public async Task<bool> IsRoleExists(string roleName) => await _roleManager.RoleExistsAsync(roleName);

}
