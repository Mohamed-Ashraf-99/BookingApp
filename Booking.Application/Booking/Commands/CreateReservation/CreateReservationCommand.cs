using Booking.Domain.Entities.Enums;
using MediatR;

namespace Booking.Application.Booking.Commands.CreateReservation;

public class CreateReservationCommand : IRequest<string>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfGuests { get; set; }
    public int? ClientId { get; set; }
    public string? PaymentIntentId { get; set; }
    public decimal Amount { get; set; }
    public List<int> RoomIds { get; set; }
    public ReservationState? State { get; set; } = ReservationState.Pending;

}
