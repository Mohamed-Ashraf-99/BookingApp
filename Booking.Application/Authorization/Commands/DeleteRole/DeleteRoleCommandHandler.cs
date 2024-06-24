using Booking.Application.Services.Authorization;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authorization.Commands.DeleteRole;

public class DeleteRoleCommandHandler(ILogger<DeleteRoleCommandHandler> _logger,
    IAuthorizationServices _authorizationServices) : IRequestHandler<DeleteRoleCommand, string>
{
    public async Task<string> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Delete role with ID: {request.Id}");

        try
        {
            var response = await _authorizationServices.DeleteRoleAsync(request.Id);
            _logger.LogInformation($"Successfully deleted role with ID: {request.Id}");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting role with ID: {request.Id}");
            throw new Exception(ex.Message, ex);
        }
    }
}
