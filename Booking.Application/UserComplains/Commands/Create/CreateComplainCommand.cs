using MediatR;

namespace Booking.Application.UserComplains.Commands.Create;

public class CreateComplainCommand : IRequest<string>
{
    public string Description { get; set; }
    public int HotelId { get; set; }
    public int OwnerId { get; set; }
    public int ClientId { get; set; }
    public DateTime? Date { get; set; } = DateTime.Now;
}
