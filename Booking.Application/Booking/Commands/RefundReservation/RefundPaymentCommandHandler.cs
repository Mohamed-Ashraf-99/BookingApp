using Booking.Application.Services.Payment;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Booking.Commands.RefundReservation;

public class RefundPaymentCommandHandler(ILogger<RefundPaymentCommandHandler> _logger, IPaymentServices _paymentService, IReservationRepository _reservationRepository) : IRequestHandler<RefundPaymentCommand, bool>
{

    public async Task<bool> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);

            if (reservation == null)
            {
                _logger.LogError($"Reservation with id {request.ReservationId} not found.");
                return false;
            }

            string paymentIntentId = reservation.PaymentIntentId;

            if (string.IsNullOrEmpty(paymentIntentId))
            {
                _logger.LogError($"PaymentIntentId not found for reservation {request.ReservationId}.");
                return false;
            }

            return await _paymentService.RefundPayment(paymentIntentId, request.AmountToRefund);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing refund payment");
            throw new Exception(ex.Message, ex);
        }
    }
}