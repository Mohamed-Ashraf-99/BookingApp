using Booking.Application.Authentication.Commands.RefreshToken;
using Booking.Application.Authentication.Commands.SignIn;
using Booking.Application.Authentication.Queries.AuthorizeUser;
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

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand refreshTokenCommand)
        {
            var response = await _mediator.Send(refreshTokenCommand);
            return Ok(response);
        }

        [HttpGet("ValidateToken")]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery authorizeUserQuery)
        {
            var response = await _mediator.Send(authorizeUserQuery);
            return Ok(response);
        }
    }
}
