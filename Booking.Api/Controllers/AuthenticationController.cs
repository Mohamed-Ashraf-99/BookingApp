using Booking.Application.Authentication.Commands.ConformResetPassword;
using Booking.Application.Authentication.Commands.Logout;
using Booking.Application.Authentication.Commands.RefreshToken;
using Booking.Application.Authentication.Commands.SignIn;
using Booking.Application.Authentication.Commands.UpdatePassword;
using Booking.Application.Authentication.Queries.AuthorizeUser;
using Booking.Application.Authentication.Queries.ConfirmEmail;
using Booking.Application.Authentication.Queries.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IMediator _mediator) : ControllerBase
{
    [HttpPost("Login")] 
    public async Task<IActionResult> Login([FromBody] SignInCommand signInCommand)
    {
        var response = await _mediator.Send(signInCommand);
        return Ok(response);
        #region Response
        //{
        //"accessToken": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQXNocmFmIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJBc2hyYWYiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJBc2hyYWZAZ21haWwuY29tIiwiUGhvbmVOdW1iZXIiOiIrMDExNDU5NTE3NjIiLCJJZCI6IjE1IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTcxOTMwODgyMiwiaXNzIjoiQm9va2luZ1Byb2plY3QiLCJhdWQiOiJXZWJTaXRlIn0.5fVBnqTSgV8V1W6uo6ZQOsB9Le2RT8XOzZikHAMiX8A",
        //"refreshToken": {
        //"userName": "Ashraf",
        //  "token": "T1ZhSVyZg0mCn1cx3pJlXJuRum6hmAIhcY77BY7Q4Fk=",
        //  "expireDate": "2024-07-14T12:47:03.0501961+03:00"
        //}
        //          }
        #endregion
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

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery confirmEmailQuery)
    {
        var response = await _mediator.Send(confirmEmailQuery);
        return Redirect("http://localhost:4200/login");

    }

    [HttpGet("ConfirmOwnerEmail")]
    public async Task<IActionResult> ConfirmOwnerEmail([FromQuery] ConfirmEmailQuery confirmEmailQuery)
    {
        var response = await _mediator.Send(confirmEmailQuery);
        return Redirect("http://localhost:4200/registrationconfirmed");

    }

    [HttpPost("SendResetPasswordRequest")]
    public async Task<IActionResult> SendResetPasswordRequest([FromForm] SendResetPasswordCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);

    }

    [HttpPost("ConfirmResetPassword")]
    public async Task<IActionResult> ConfirmResetPassword([FromForm] ConfirmResetPasswordQuery command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);

    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}

