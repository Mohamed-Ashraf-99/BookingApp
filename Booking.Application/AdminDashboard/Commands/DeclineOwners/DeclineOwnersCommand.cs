
using MediatR;

namespace Booking.Application.AdminDashboard.Commands.DeclineOwners;

public class DeclineOwnersCommand(int id) : IRequest<string>
{
    public int Id { get; set; } = id;
}
