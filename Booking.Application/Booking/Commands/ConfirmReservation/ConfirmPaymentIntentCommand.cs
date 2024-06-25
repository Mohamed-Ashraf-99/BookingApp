using MediatR;
namespace Booking.Application.Booking.Commands.ConfirmReservation;

public class ConfirmPaymentIntentCommand : IRequest<bool>
{
    public string PaymentIntentId { get; set; }
    public string PaymentMethodId { get; set; }
}
