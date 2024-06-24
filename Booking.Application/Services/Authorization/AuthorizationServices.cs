using AutoMapper;
using Booking.Application.Authorization.Commands.EditRole.Dto;
using Booking.Application.Authorization.Queries.GetAllRoles.Dto;
using Booking.Application.Authorization.Queries.ManageUserRoles;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Services.Authorization;

public class AuthorizationServices(RoleManager<IdentityRole<int>> _roleManager,
    ILogger<AuthorizationServices> _logger,
    IMapper _mapper,
    UserManager<User> _userManager) : IAuthorizationServices
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

    public async Task<string> EditRoleAsync(EditRoleDto roleDto)
    {
        try
        {
            var role = await _roleManager.FindByIdAsync(roleDto.Id.ToString());
            if (role is null)
                throw new NotFoundException($"Role with ID {roleDto.Id} not found");

            role.Name = roleDto.RoleName;
            role.NormalizedName = roleDto.RoleName.ToUpper();
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
                return "Succeeded";

            return string.Join("; ", result.Errors.Select(e => e.Description));
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            return $"Error: {ex.Message}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while editing the role");
            return "An unexpected error occurred. Please try again later.";
        }
    }

    public async Task<string> DeleteRoleAsync(int roleId)
    {
        try
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                _logger.LogWarning($"Role with ID {roleId} not found.");
                throw new NotFoundException($"Role with ID {roleId} not found");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogWarning($"Failed to delete role with ID {roleId}: {errors}");
                return errors;
            }

            _logger.LogInformation($"Successfully deleted role with ID {roleId}");
            return "Succeeded";
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            return $"Error: {ex.Message}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while deleting the role");
            return "An unexpected error occurred. Please try again later.";
        }
    }


    public async Task<bool> IsRoleExists(string roleName)
        => await _roleManager.RoleExistsAsync(roleName);

    public async Task<IEnumerable<GetRolesDto>> GetAllRolesAsync()
    {
        var rolesList = await _roleManager.Roles.ToListAsync();
        if (rolesList is null || !rolesList.Any())
            throw new NotFoundException($"There are no roles found");

        var result = _mapper.Map<IEnumerable<GetRolesDto>>(rolesList);
        return result;
        throw new NotImplementedException();
    }

    public async Task<GetRolesDto> GetRoleByIdAsync(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        if (role == null)
        {
            _logger.LogWarning($"Role with ID {roleId} not found.");
            throw new NotFoundException($"Role with ID {roleId} not found");
        }
        var result = _mapper.Map<GetRolesDto>(role);
        return result;
    }

    public async Task<ManageUserRolesDto> GetUserRolesAsync(string userId)
    {
        try
        {
            if (!int.TryParse(userId, out var parsedUserId))
            {
                throw new ArgumentException("Invalid user ID format", nameof(userId));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new NotFoundException($"User with ID = {userId} not found");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();

            var rolesList = roles.Select(role => new UserRoles
            {
                Id = role.Id,
                Name = role.Name,
                HasRole = userRoles.Contains(role.Name)
            }).ToList();

            return new ManageUserRolesDto
            {
                UserId = parsedUserId,
                Roles = rolesList
            };
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "User not found.");
            throw new NotFoundException(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Invalid argument provided.");
            throw new ArgumentException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving user roles.");
            throw new Exception("An error occurred while retrieving user roles.", ex);
        }
    }

    public async Task<string> UpdateUserRoles(ManageUserRolesDto request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            throw new NotFoundException($"User with ID = {request.UserId} not found");

        var userRoles = await _userManager.GetRolesAsync(user);
        var removedRoles = await _userManager.RemoveFromRolesAsync(user, userRoles);
        if (!removedRoles.Succeeded)
            return "Failed";

        var selectedRoles = request.Roles.Where(x => x.HasRole == true).Select(x => x.Name);
        //Add Roles

        var addRolesResult = await _userManager.AddToRolesAsync(user,selectedRoles);
        if (!addRolesResult.Succeeded)
            return "Failed";

        return "Succeeded";
    }
}
