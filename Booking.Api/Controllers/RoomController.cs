using Booking.Application.ApplicationUser.Commands.OwnerCrud.AddRooms;
using Booking.Application.ApplicationUser.Commands.OwnerCrud.DeleteRoom;
using Booking.Application.ApplicationUser.Queries.GetAllHotelReviews;
using Booking.Application.ApplicationUser.Queries.GetRoomsCountByHotelId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetAllHotelReviews([FromBody] AddRoomsCommand addRoomsCommand)
        {
            var room = await _mediator.Send(addRoomsCommand);

            if (room != null )
            {
                return Ok(room);

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{hotelId:int}")]
        public async Task<IActionResult> GetRoomCount(int hotelId)
        {

            var query = new GetRoomsCountByHotelIdListQuery(hotelId);
            var RoomCount = await _mediator.Send(query);

            if (RoomCount != null )
            {
                return Ok(RoomCount);

            }
            else
            {
                return NotFound("No Rooms found .");
            }
        }


        

    }
}
