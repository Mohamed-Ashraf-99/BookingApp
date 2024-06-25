using Booking.Application.Booking.Commands.ConfirmReservation;
using Booking.Application.Services.Payment;
using MediatR;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Application.Booking.Commands.ConfirmPaymentIntent
{
    public class ConfirmPaymentIntentCommandHandler(IConfiguration _configuration,
        IPaymentServices _paymentIntentService) : IRequestHandler<ConfirmPaymentIntentCommand, bool>
    {

        public async Task<bool> Handle(ConfirmPaymentIntentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool result = await _paymentIntentService.ConfirmPaymentIntent(request.PaymentIntentId, request.PaymentMethodId);
                return result;
            }
            catch (StripeException ex)
            {
                // Handle Stripe specific exceptions
                throw new ApplicationException($"Error confirming payment intent: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new ApplicationException($"Unexpected error confirming payment intent: {ex.Message}");
            }
        }
    }
}
