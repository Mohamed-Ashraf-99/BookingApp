using Booking.Application.Booking.Commands.ConfirmReservation;
using Booking.Application.Booking.Commands.CreateReservation;
using Booking.Application.Booking.Commands.RefundReservation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "User")]
    public class ReservationsController(IMediator _mediator,
        ILogger<ReservationsController> _logger) : ControllerBase
    {
        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result == "Succeeded")
                {
                    return Ok(new { Message = "Reservation created successfully." });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to create reservation." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating reservation.");
                return StatusCode(500, new { Message = "An error occurred while creating reservation." });
            }
        }

        [HttpPost("refund-payment")]
        public async Task<IActionResult> RefundPayment(int reservationId, [FromBody] RefundPaymentCommand command)
        {
            if (command.ReservationId != reservationId)
            {
                return BadRequest(new { Message = "ReservationId in the URL does not match with the request body." });
            }

            try
            {
                bool result = await _mediator.Send(command);

                if (result)
                {
                    return Ok(new { Message = "Payment refunded successfully." });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to refund payment." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refunding payment.");
                return StatusCode(500, new { Message = "An error occurred while refunding payment." });
            }
        }

        [HttpPost("confirm-payment-intent")]
        public async Task<IActionResult> ConfirmPaymentIntent([FromBody] ConfirmPaymentIntentCommand command)
        {
            try
            {
                bool result = await _mediator.Send(command);
                if (result)
                    return Ok("Payment intent confirmed successfully");
                else
                    return BadRequest("Failed to confirm payment intent");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming payment intent");
                return StatusCode(500, "An error occurred while confirming payment intent");
            }
        }
    }
}
