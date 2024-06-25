using Azure.Core;
using Booking.Application.ApplicationUser.Commands.AddCommentToHotel;
using Booking.Application.ApplicationUser.Queries.GetAllReviews;
using Booking.Application.ApplicationUser.Queries.GetTrendingHotels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController (IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {

            var query = new GetAllReviewsListQuery();
            var reviews = await _mediator.Send(query);

            if (reviews != null && reviews.Any())
            {
                return Ok(reviews);
                
            }
            else
            {
                return NotFound("No reviews found .");
            }
        }


        [HttpPost("{clientId}/{hotelId}")]
        public async Task<IActionResult> AddCommentToHotel(int clientId, int hotelId, [FromBody] AddCommentToHotelCommand request)
        {
            var command = new AddCommentToHotelCommand(clientId, hotelId)
            {
                CommentText = request.CommentText,
                Rate = request.Rate
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }


}
