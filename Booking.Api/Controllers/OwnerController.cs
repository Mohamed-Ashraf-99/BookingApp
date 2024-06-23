using Booking.Application.ApplicationUser.Queries.GetHotelsByOwnerId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("{ownerId:int}/hotels")]
        public async Task<IActionResult> GetHotelByOwnerId(int ownerId)
        {
            var query = new GetHotelsByOwnerIdListQuery(ownerId);
            var hotel = await _mediator.Send(query);

            if (hotel != null)
            {
                return Ok(hotel);
            }
            else
            {
                return NotFound($"No hotels found for the owner with ID {ownerId}.");
            }
        }
    }
}
