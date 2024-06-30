using Booking.Domain.Entities.Identity;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Numerics;

namespace Booking.Application.ClientProfile.Commands.DeleteProfile;

public class DeleteProfileCommandHandler(ILogger<DeleteProfileCommandHandler> _logger,
    UserManager<User> _userManager, IClientRepository _clientRepository) : IRequestHandler<DeleteProfileCommand, string>
{
    public async Task<string> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var client = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (client == null)
            {
                _logger.LogWarning($"User with ID {request.UserId} not found.");
                return "User not found";
            }

            var deleteClient = await _clientRepository.DeleteAsync(request.UserId);

            if (!deleteClient)
            {
                _logger.LogError($"Failed to delete client with ID {request.UserId}.");
                return "Failed to delete client";
            }

            var result = await _userManager.DeleteAsync(client);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                _logger.LogError($"Failed to delete user {client.UserName}. Errors: {errors}");
                return $"Failed to delete user {client.UserName}";
            }

            _logger.LogInformation($"User {client.UserName} and associated client profile deleted successfully.");
            return "Client profile deleted successfully";
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting client profile: {ex.Message}");
            return "An error occurred while deleting client profile. Please try again later.";
        }
    }
}
