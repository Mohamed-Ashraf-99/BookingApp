using MediatR;

namespace Booking.Application.Booking.Commands.RefundReservation;

public class RefundPaymentCommand : IRequest<bool>
{
    public int ReservationId { get; set; }
    public decimal AmountToRefund { get; set; }
}
