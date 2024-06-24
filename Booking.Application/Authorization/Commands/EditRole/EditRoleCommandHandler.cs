using AutoMapper;
using Booking.Application.Services.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authorization.Commands.EditRole;

public class EditRoleCommandHandler(ILogger<EditRoleCommandHandler> _logger,
    IMapper _mapper,
    RoleManager<IdentityRole<int>> _roleManager,
    IAuthorizationServices _authorizationServices) : IRequestHandler<EditRoleCommand, string>
{
    public async Task<string> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Edit role with ID: {request.Id}");

        try
        {
            var response = await _authorizationServices.EditRoleAsync(request);
            _logger.LogInformation($"Successfully edited role with ID: {request.Id}");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while editing role with ID: {request.Id}");
            throw new Exception(ex.Message, ex);
        }
    }
}
