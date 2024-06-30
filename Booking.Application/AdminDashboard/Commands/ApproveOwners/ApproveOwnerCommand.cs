
using MediatR;

namespace Booking.Application.AdminDashboard.Commands.ApproveOwners;

public class ApproveOwnerCommand(int id) : IRequest<string>
{
    public int Id { get; set; } = id;
}
