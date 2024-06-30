using Booking.Application.AdminDashboard.Commands.ApproveOwners;
using Booking.Application.AdminDashboard.Queries.GetAllUnVerifiedOwners;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("GetUnVerifiedOwners")]
        public async Task<IActionResult> GetUnVerifiedOwners()
        {
            var response =await _mediator.Send(new GetUnVerifiedOwnersQuery());
            return Ok(response);
        }

        [HttpPost("ApproveOwner")]
        public async Task<IActionResult> ApproveOwner([FromQuery] int id)
        {
            var response = await _mediator.Send(new ApproveOwnerCommand(id));
            return Ok(response);
        }
    }
}
