using Booking.Application.Booking;
using Booking.Application.Booking.Commands.Checkout;
using Booking.Application.Booking.Commands.ConfirmReservation;
using Booking.Application.Booking.Commands.CreateReservation;
using Booking.Application.Booking.Commands.RefundReservation;
using Booking.Application.Services.Payment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe.Tax;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ReservationsController(IMediator _mediator,
        ILogger<ReservationsController> _logger,
        IPaymentServices _paymentServices) : ControllerBase
    {
        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateSessionDto command)
        {
            var result = await _paymentServices.CreateCheckoutSessionAsync(command);
            return Ok(new {text = result });
        }

        [HttpPost("Succeeded")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateReservationCommand command)
        {
            var result = await _paymentServices.Success(command);
            return Ok(result);
        }

    }
}
