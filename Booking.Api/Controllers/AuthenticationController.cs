using Booking.Application.Authentication.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] SignInCommand signInCommand)
        {
            var response = await _mediator.Send(signInCommand);
            return Ok(response);
        }
    }
}
