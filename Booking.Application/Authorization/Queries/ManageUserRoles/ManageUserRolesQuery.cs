using MediatR;

namespace Booking.Application.Authorization.Queries.ManageUserRoles;

public class ManageUserRolesQuery(int id) : IRequest<ManageUserRolesDto>
{
    public string UserId { get; set; } = id.ToString();
}
