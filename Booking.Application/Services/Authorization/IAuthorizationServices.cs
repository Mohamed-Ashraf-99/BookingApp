using Booking.Application.Authorization.Commands.EditRole;
using Booking.Application.Authorization.Commands.EditRole.Dto;
using Booking.Application.Authorization.Queries.GetAllRoles.Dto;
using Booking.Application.Authorization.Queries.ManageUserRoles;

namespace Booking.Application.Services.Authorization;

public interface IAuthorizationServices
{
    Task<string> AddRoleAsync(string roleName);
    Task<bool> IsRoleExists(string roleName);
    Task<string> EditRoleAsync(EditRoleDto roleDto);
    Task<string> DeleteRoleAsync(int RoleId);
    Task<IEnumerable<GetRolesDto>> GetAllRolesAsync();
    Task<GetRolesDto> GetRoleByIdAsync(int roleId);
    Task<ManageUserRolesDto> GetUserRolesAsync(string userId);
    Task<string> UpdateUserRoles(ManageUserRolesDto request);
}
