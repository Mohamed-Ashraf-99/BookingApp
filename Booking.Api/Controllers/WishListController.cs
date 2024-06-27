using Booking.Application.ApplicationUser.Commands.AddHotelsToWishList;
using Booking.Application.ApplicationUser.Queries.GetHotelById;
using Booking.Application.ApplicationUser.Queries.GetWishListByClientId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController (IMediator _mediator) : ControllerBase
    {
        [HttpGet("{clientId:int}")]
        public async Task<IActionResult> GetWishListById(int clientId)
        {
            var query = new GetWishListByClientIdListQuery(clientId);
            var wishlist = await _mediator.Send(query);

            if (wishlist != null)
            {
                return Ok(wishlist);
            }
            else
            {
                return NotFound($"No wishlist found for the client with ID {clientId}.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> InsertHotelToWishList([FromBody] AddHotelsToWishListCommand addHotelsToWishListCommand)
        {
           // var query = new AddHotelsToWishListCommand(clientId,hotelId);
            var wishlist = await _mediator.Send(addHotelsToWishListCommand);

            if (wishlist != null)
            {
                return Ok(wishlist);
            }
            else
            {
                return BadRequest();
         
            }
        }
    }
}
