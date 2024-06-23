using Booking.Application.Authorization.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AuthorizationController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("Role")]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
