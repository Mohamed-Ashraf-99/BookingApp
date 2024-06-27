using Booking.Application.UserComplains.Commands.Create;
using Booking.Application.UserComplains.Queries.GetHotelComplains;
using Booking.Application.UserComplains.Queries.GetUsersComplains;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ComplainsController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateComplain([FromBody] CreateComplainCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetHotelComplains(int hotelId)
        {

                var result = await _mediator.Send(new GetHotelComplainsQuery { HotelId = hotelId });
                return Ok(result);
                 
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserComplains(int userId)
        {
                var result = await _mediator.Send(new GetUserComplainsQuery { UserId = userId });
                return Ok(result);
        }
    }
}
