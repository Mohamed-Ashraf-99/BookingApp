using Booking.Application.Emails.Commands.SendEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendEmail([FromForm] SendEmailCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}
