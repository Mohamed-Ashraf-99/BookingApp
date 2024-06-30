using Booking.Application.Booking.Queries.ClientReservations;
using Booking.Application.ClientProfile.Commands.DeleteProfile;
using Booking.Application.ClientProfile.Commands.UpdateProfile;
using Booking.Application.ClientProfile.Queries.GetClientData;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientProfileController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("GetClientData")]
        public async Task<IActionResult> GetClientData([FromQuery]int userId)
        {
            var response = await _mediator.Send(new UserDataQuery(userId));
            return Ok(response);
        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileCommand command)
        {
            var response = await _mediator.Send(command);

            if (response == "Profile updated successfully")
                return Ok(response);
            else
                return BadRequest(response);         
        }

        [HttpGet("GetClientReservations")]
        public async Task<IActionResult> GetClientReservations([FromQuery]int userId)
        {
            var response = await _mediator.Send(new ClientReservationsQuery(userId));
            return Ok(response);
        }

        [HttpDelete("DeleteProfile")]
        public async Task<IActionResult> DeleteProfile([FromQuery] int userId)
        {
            var response = await _mediator.Send(new DeleteProfileCommand(userId));
            return Ok(response);
        }

    }
}
