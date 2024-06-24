using Booking.Application.Authorization.Commands.EditRole;
using Booking.Application.Authorization.Commands.EditRole.Dto;

namespace Booking.Application.Services.Authorization;

public interface IAuthorizationServices
{
    Task<string> AddRoleAsync(string roleName);
    Task<bool> IsRoleExists(string roleName);
    Task<string> EditRoleAsync(EditRoleDto roleDto);
}
