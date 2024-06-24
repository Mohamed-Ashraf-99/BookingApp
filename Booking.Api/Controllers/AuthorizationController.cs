using Booking.Application.Authorization.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthorizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Role")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}
