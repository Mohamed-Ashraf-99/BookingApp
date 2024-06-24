using MediatR;

namespace Booking.Application.Authorization.Commands.CreateRole;

public class CreateRoleCommand : IRequest<string>
{
    public string RoleName { get; set; }
}
