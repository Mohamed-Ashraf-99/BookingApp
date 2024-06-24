using Booking.Application.Authorization.Queries.GetAllRoles.Dto;
using MediatR;

namespace Booking.Application.Authorization.Queries.GitRoleById;

public class GetRoleByIdQuery(int id) : IRequest<GetRolesDto>
{
    public int Id { get; set; } = id;
}
