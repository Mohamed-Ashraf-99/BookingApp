using AutoMapper;
using Booking.Application.Services.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authorization.Commands;

public class CreateRoleCommandHandler(ILogger<CreateRoleCommandHandler> _logger,
    IAuthorizationServices _authorizationServices) : IRequestHandler<CreateRoleCommand, string>
{
    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Attempting to create role: {request.RoleName}");

        try
        {
            if (await _authorizationServices.IsRoleExists(request.RoleName))
            {
                _logger.LogWarning($"Role [{request.RoleName}] already exists.");
                return "Role already exists!";
            }
            var result = await _authorizationServices.AddRoleAsync(request.RoleName);
            if (result == "Succeeded")
            {
                _logger.LogInformation($"Role [{request.RoleName}] created successfully.");
                return "Succeeded";
            }
            _logger.LogWarning($"Failed to create role [{request.RoleName}].");
            return "Failed";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while creating role [{request.RoleName}].");
            return $"An error occurred: {ex.Message}";
        }
    }
}
