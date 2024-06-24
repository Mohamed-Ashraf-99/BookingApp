using Booking.Application.Services.Authorization;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authorization.Commands.UpdateUserRoles;

public class UpdateUserRolesCommandHandler(ILogger<UpdateUserRolesCommandHandler> _logger,
    IAuthorizationServices _authorizationServices) : IRequestHandler<UpdateUserRolesCommand, string>
{
    public async Task<string> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        var response = await _authorizationServices.UpdateUserRoles(request);
        return response;
    }
}
