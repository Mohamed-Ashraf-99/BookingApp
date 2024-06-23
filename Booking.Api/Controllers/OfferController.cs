using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers;
using Booking.Application.ApplicationUser.Queries.GetOffersByHotelId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController(IMediator _mediator) : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> GetAllHotelOffers()
        {

            var query = new GetAllHotelOffersListQuery();
            var offers = await _mediator.Send(query);

            if (offers != null && offers.Any())
            {
                return Ok(offers);

            }
            else
            {
                return NotFound("No offers found .");
            }
        }

        [HttpGet("{hotelId:int}")]
        public async Task<IActionResult> GetOffersByHotelId(int hotelId)
        {
            var query = new GetOffersByHotelIdListQuery(hotelId);
            var offers = await _mediator.Send(query);

            if (offers != null && offers.Any())
            {
                return Ok(offers);
            }
            else
            {
                return NotFound($"No offers found for the hotel with ID {hotelId}.");
            }
        }
    }
}
