using MediatR;

namespace Booking.Application.ClientProfile.Commands.DeleteProfile;

public class DeleteProfileCommand(int id) : IRequest<string>
{
    public int UserId { get; set; } = id;
}
