using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Booking.Application.Authorization.Commands.CreateRole;
using Booking.Application.Authorization.Commands.EditRole;
using Booking.Application.Authorization.Commands.DeleteRole;
using Booking.Application.Authorization.Queries.GetAllRoles;
using Booking.Application.Authorization.Queries.GitRoleById;
using Booking.Application.Authorization.Queries.ManageUserRoles;
using Booking.Application.Authorization.Commands.UpdateUserRoles;

namespace YourNamespace.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AuthorizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/Authorization/Role
    [HttpPost("Role")]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("Role")]
    public async Task<IActionResult> Edit([FromBody] EditRoleCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("Role")]
    public async Task<IActionResult> Delete([FromBody] DeleteRoleCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("Role")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetRoleListQuery());
        return Ok(response);
    }

    [HttpGet("Role/{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var response = await _mediator.Send(new GetRoleByIdQuery(id));
        return Ok(response);
    }

    [HttpGet("Role/User/{id}")]
    public async Task<IActionResult> GetUserRoles([FromRoute] int id)
    {
        var response = await _mediator.Send(new ManageUserRolesQuery(id));
        return Ok(response);
    }


    [HttpPut("Role/User")]
    public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}
