using Booking.Application.ApplicationUser.Commands.ChangePassword;
using Booking.Application.ApplicationUser.Commands.DeleteUser;
using Booking.Application.ApplicationUser.Commands.Register;
using Booking.Application.ApplicationUser.Commands.UpdateUser;
using Booking.Application.ApplicationUser.Queries.GetAllUsers;
using Booking.Application.ApplicationUser.Queries.GetUserById;
using Booking.Application.RegisterAsOwner.Commands.OwnerRegister;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerUserCommand)
    {
        var response = await _mediator.Send(registerUserCommand);
        if (response is not null)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPost("OwnerRegister")]
    public async Task<IActionResult> OwnerRegister([FromForm] OwnerRegisterCommand registerUserCommand)
    {
        var response = await _mediator.Send(registerUserCommand);
        if (response is not null)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _mediator.Send(new GetUserListQuery());
        return Ok(users);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetUserById(int Id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(Id));
        return Ok(user);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update([FromBody]UpdateUserCommand updateUserCommand)
    {
        if (ModelState.IsValid)
        {
            var result = await _mediator.Send(updateUserCommand);
            return Ok(result);
        }
        return BadRequest();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] int Id)
    {
        var result =await _mediator.Send(new DeleteUserCommand(Id));
        return NoContent();
    }

    [HttpPut("ChangePassword/{Id}")]
    public async Task<IActionResult> ChangePassword([FromRoute]int Id, [FromBody] ChangePasswordCommand changePasswordCommand)
    {
        changePasswordCommand.Id = Id;
        var result = await _mediator.Send(changePasswordCommand);
        return Ok(result);
    }
}
