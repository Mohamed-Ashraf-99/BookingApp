using Booking.Application.Authorization.Queries.ManageUserRoles;
using MediatR;

namespace Booking.Application.Authorization.Commands.UpdateUserRoles;

public class UpdateUserRolesCommand : ManageUserRolesDto, IRequest<string>
{

}
