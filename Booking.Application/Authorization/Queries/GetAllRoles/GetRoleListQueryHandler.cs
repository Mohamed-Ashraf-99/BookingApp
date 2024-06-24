using Booking.Application.Authorization.Queries.GetAllRoles.Dto;
using Booking.Application.Services.Authorization;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authorization.Queries.GetAllRoles;

public class GetRoleListQueryHandler(ILogger<GetRoleListQueryHandler> _logger,
    IAuthorizationServices _authorizationServices) : IRequestHandler<GetRoleListQuery, IEnumerable<GetRolesDto>>
{
    public async Task<IEnumerable<GetRolesDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        var response = await _authorizationServices.GetAllRolesAsync();
        return response;
    }
}
