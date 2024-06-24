using Booking.Application.ApplicationUser.Queries.GetAllCityHotels;
using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers;
using Booking.Application.ApplicationUser.Queries.GetAllHotels;
using Booking.Application.ApplicationUser.Queries.GetHotelById;
using Booking.Application.ApplicationUser.Queries.GetHotelsByOwnerId;
using Booking.Application.ApplicationUser.Queries.GetOffersByHotelId;
using Booking.Application.ApplicationUser.Queries.GetTrendingHotels;
using Booking.Application.ApplicationUser.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController (IMediator _mediator) : ControllerBase
    {
        [HttpGet("{City:alpha}")]
        public async Task<IActionResult> GetHotelByCity(string City)
        {
            
            var query = new GetCityHotelsListQuery(City); 
            var hotels = await _mediator.Send(query);

            if (hotels != null && hotels.Any())
            {
                return Ok(hotels);

            }
            else
            {
                return NotFound("No hotels found for the specified city.");
            }
        }


        [HttpGet("allHotels")]
        public async Task<IActionResult> GetAllHotels()
        {

            var query = new GetAllHotelsListQuery();
            var hotels = await _mediator.Send(query);

            if (hotels != null && hotels.Any())
            {
                return Ok(hotels);

            }
            else
            {
                return NotFound("No offers found .");
            }
        }

        [HttpGet("{hotelId:int}")]
        public async Task<IActionResult> GetHotelById(int hotelId)
        {
            var query = new GetHotelByIdListQuery(hotelId);
            var hotel = await _mediator.Send(query);

            if (hotel != null )
            {
                return Ok(hotel);
            }
            else
            {
                return NotFound($"No hotels found for the hotel with ID {hotelId}.");
            }
        }

        [HttpGet("trendingHotels")]

        public async Task<IActionResult> GetAllTrendingHotels()
        {

            var query = new GetTrendingHotelsListQuery();
            var hotels = await _mediator.Send(query);

            if (hotels != null && hotels.Any())
            {
                return Ok(hotels);

            }
            else
            {
                return NotFound("No hotels found .");
            }
        }

    }
}
