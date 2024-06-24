using MediatR;

namespace Booking.Application.Authorization.Commands.DeleteRole;

public class DeleteRoleCommand(int id) : IRequest<string>
{
    public int Id { get; set; } = id;

}
