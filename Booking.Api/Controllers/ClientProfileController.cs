using Booking.Application.ClientProfile.Commands.UpdateProfile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientProfileController(IMediator _mediator) : ControllerBase
    {
        //[HttpGet("GetClientData")]
        //public async Task<IActionResult> GetClientData([FromRoute]int clientId)
        //{

        //}

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        //[HttpGet("UpdateProfile")]
        //public async Task<IActionResult> GetClientReservations([FromRoute]int clientId)
        //{

        //}

        //[HttpDelete("UpdateProfile")]
        //public async Task<IActionResult> DeleteProfile([FromRoute] int clientId)
        //{

        //}

    }
}
