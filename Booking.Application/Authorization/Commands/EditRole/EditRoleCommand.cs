using Booking.Application.Authorization.Commands.EditRole.Dto;
using MediatR;

namespace Booking.Application.Authorization.Commands.EditRole;

public class EditRoleCommand : EditRoleDto , IRequest<string>
{
 
}
