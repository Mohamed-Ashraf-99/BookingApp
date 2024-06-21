using MediatR;

namespace Booking.Application.ApplicationUser.Commands.DeleteUser;

public class DeleteUserCommand(int id) : IRequest<string>
{
    public int UserId { get; set; } = id;
}
