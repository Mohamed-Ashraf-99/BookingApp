
using AutoMapper;
using Booking.Application.ClientProfile.Commands.UpdateProfile;
using Booking.Application.Services.FileUpload;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Tls;

namespace Booking.Application.Services.ClientProfile;

public class ClientProfileService(ILogger<ClientProfileService> _logger, 
    IClientRepository _clientRepository, IMapper _mapper,
    IHttpContextAccessor _httpContextAccessor, IFileService _fileService,
    UserManager<User> _userManager) : IClientProfileService
{
    public async Task<string> UpdateAsync(UpdateProfileCommand command)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(command.UserId.ToString());
            if (user is null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", command.UserId);
                return "User not found";
            }

            _mapper.Map(command, user);

            if (command.Image != null)
            {
                var imageUrl = await _fileService.UploadImage("ClientImage", command.Image);
                if (imageUrl.StartsWith("Failed"))
                {
                    _logger.LogWarning("Failed to upload image for user with ID {UserId}.", command.UserId);
                    return "Failed to upload image";
                }

                var context = _httpContextAccessor.HttpContext.Request;
                var baseUrl = $"{context.Scheme}://{context.Host}/";
                user.Image = baseUrl + imageUrl.TrimStart('/');
            }

            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("User profile with ID {UserId} updated successfully.", command.UserId);
                return "Profile updated successfully";
            }

            _logger.LogError("Failed to update user profile with ID {UserId}.", command.UserId);
            return "Failed to update profile";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the profile for user with ID {UserId}.", command.UserId);
            return "Internal Server Error. Please try again later.";
        }
    }

}
