using Booking.Application.Booking.Commands.CreateReservation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
        {
            var result = await _mediator.Send(command);
            //return CreatedAtAction(nameof(GetReservationById), new { id = result }, result);
            return Ok(result);
        }
    }
}
