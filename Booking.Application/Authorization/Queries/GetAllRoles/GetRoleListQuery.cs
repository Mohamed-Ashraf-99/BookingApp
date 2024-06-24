using Booking.Application.Authorization.Queries.GetAllRoles.Dto;
using MediatR;

namespace Booking.Application.Authorization.Queries.GetAllRoles;

public class GetRoleListQuery : IRequest<IEnumerable<GetRolesDto>>
{
}
