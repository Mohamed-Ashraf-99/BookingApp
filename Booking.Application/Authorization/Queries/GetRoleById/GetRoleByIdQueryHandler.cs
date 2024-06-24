using Booking.Application.Authorization.Queries.GetAllRoles.Dto;
using Booking.Application.Services.Authorization;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authorization.Queries.GitRoleById;

public class GetRoleByIdQueryHandler(ILogger<GetRoleByIdQueryHandler> _logger,
    IAuthorizationServices _authorizationServices) : IRequestHandler<GetRoleByIdQuery, GetRolesDto>
{
    public async Task<GetRolesDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Get Role with ID = {request.Id}");
        var response = await _authorizationServices.GetRoleByIdAsync(request.Id);
        return response;
    }
}
