using Booking.Application.Services.Authorization;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authorization.Queries.ManageUserRoles;

public class ManageUserRolesQueryHandler(ILogger<ManageUserRolesQueryHandler> _logger,
    IAuthorizationServices _authorizationServices) : IRequestHandler<ManageUserRolesQuery, ManageUserRolesDto>
{
    public async Task<ManageUserRolesDto> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"ManageUserRolesQuery handler: {request}");
        var result = await _authorizationServices.GetUserRolesAsync(request.UserId);
        return result;
    }
}
