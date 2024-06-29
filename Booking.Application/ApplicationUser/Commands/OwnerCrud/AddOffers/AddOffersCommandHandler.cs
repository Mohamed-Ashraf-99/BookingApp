using Booking.Application.ApplicationUser.Commands.OwnerCrud.AddRooms;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.OwnerCrud.AddOffers
{
    public class AddOffersCommandHandler(ILogger<AddOffersCommandHandler> _logger,
    IOfferRepository offerRepository, IOwnerRepository ownerRepository) : IRequestHandler<AddOffersCommand, string>
    {
        public async Task<string> Handle(AddOffersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.userId = await ownerRepository.GetOwnerIdByUserId(request.userId);
                if (request.userId == null)
                {
                    _logger.LogWarning($"Owner not found for user ID {request.userId}");
                    return "Owner not found";
                }

                var offer = new Offer
                {

                    Description = request.description,
                    StartDate = request.startDate,
                    EndDate = request.endDate,
                    Discount = request.discount,
                    IsDeleted = false,
                    OwnerId = request.userId,
                    HotelId= request.hotelId
                };

                 await offerRepository.AddOffer(offer);


                _logger.LogInformation($"Offer added successfully for owner {request.userId}.");
                return $"Offer added successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the offer");
                return ex.Message;
            }
        }
    }
}
