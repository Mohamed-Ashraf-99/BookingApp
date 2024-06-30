using Booking.Application.ApplicationUser.Commands.AddHotelsToWishList;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.AddCommentToHotel
{
    public class AddCommentToHotelCommandHandler(ILogger<AddCommentToHotelCommandHandler> _logger,
    IReviewsRepository reviewsRepository, IClientRepository clientRepository) : IRequestHandler<AddCommentToHotelCommand, string>
    {
        public async Task<string> Handle(AddCommentToHotelCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling AddCommentToHotelCommand: {Command}", request);

            try
            {
                var review = new Reviews
                {
                    Rate = request.Rate,
                    Comment = request.CommentText,
                    HotelId = request.hotelId,
                    ClientId = await clientRepository.GetClientIdByUserId(request.userId),
                    Date = DateTime.UtcNow
                };

                _logger.LogInformation("Created Review object: {Review}", review);

                await reviewsRepository.InsertReview(review);
                _logger.LogInformation("Inserted Review into repository for HotelId: {HotelId}, ClientId: {ClientId}", request.hotelId, request.userId);

                var resultMessage = "Review added successfully";
                _logger.LogInformation(resultMessage);
                return resultMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling AddCommentToHotelCommand for HotelId: {HotelId}, ClientId: {ClientId}", request.hotelId, request.userId);
                throw;
            }
        }
    }
}
