using MediatR;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Booking.Commands.Checkout
{

    public class CreateCheckoutSessionCommandHandler : IRequestHandler<CreateCheckoutSessionCommand, CreateCheckoutSessionResponse>
    {
        private readonly IConfiguration _configuration;
        //private readonly StripeSettings _stripeSettings;

        public CreateCheckoutSessionCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            //_stripeSettings = stripeSettings;
        }

        public async Task<CreateCheckoutSessionResponse> Handle(CreateCheckoutSessionCommand request, CancellationToken cancellationToken)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = request.SuccessUrl,
                CancelUrl = request.FailureUrl,
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "subscription",
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = request.Amount.ToString(),
                    Quantity = 1,
                }
            }
            };

            var service = new SessionService();

            try
            {
                var session = await service.CreateAsync(options);
                return new CreateCheckoutSessionResponse
                {
                    SessionId = session.Id,
                    PublicKey = _configuration["StripeSettings:PublishableKey"];
                };
            }
            catch (StripeException ex)
            {
                Console.WriteLine(ex.StripeError.Message);
                throw new Exception(ex.StripeError.Message);
            }
        }
    }

}
