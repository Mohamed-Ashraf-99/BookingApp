﻿using Booking.Application.Authorization.Commands.EditRole.Dto;
using Booking.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Services.Authorization;

public class AuthorizationServices(RoleManager<IdentityRole<int>> _roleManager,
    ILogger<AuthorizationServices> _logger) : IAuthorizationServices
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


    public async Task<bool> IsRoleExists(string roleName) => await _roleManager.RoleExistsAsync(roleName);

}
