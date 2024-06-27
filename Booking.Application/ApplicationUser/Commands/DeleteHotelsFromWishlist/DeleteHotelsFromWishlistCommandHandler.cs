using Booking.Application.ApplicationUser.Commands.DeleteUser;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.DeleteHotelsFromWishlist
{
    public class DeleteHotelsFromWishlistCommandHandler(ILogger<DeleteHotelsFromWishlistCommandHandler> _logger,
    IWishListRepository wishListRepository) : IRequestHandler<DeleteHotelsFromWishlistCommand, string>
    {


        public async Task<string> Handle(DeleteHotelsFromWishlistCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var wishList = await wishListRepository.GetWishListByClientIdAsync(request.clientId);

                if (wishList == null)
                {
                    _logger.LogWarning($"No wish list found for client with ID {request.clientId}");
                    return "No wish lists for this client.";
                }

                var hotelWishList = wishList.HotelWishLists.FirstOrDefault(h => h.HotelsId == request.hotelId);

                if (hotelWishList == null)
                {
                    _logger.LogWarning($"Hotel with ID {request.hotelId} not found in the wish list for client ID {request.clientId}");
                    return "Hotel not found in the wish list.";
                }

                _logger.LogInformation($"Removing hotel with ID {request.hotelId} from the wish list for client ID {request.clientId}");
                await wishListRepository.DeleteUserWishList(hotelWishList);

                // Check if the wish list is empty after removal
                wishList = await wishListRepository.GetWishListByClientIdAsync(request.clientId);
                if (wishList != null && !wishList.HotelWishLists.Any())
                {
                    _logger.LogInformation($"Deleting empty wish list for client ID {request.clientId}");
                    await wishListRepository.DeleteWishListAsync(wishList);
                }

                _logger.LogInformation($"Successfully deleted hotel with ID {request.hotelId} from the wish list for client ID {request.clientId}");
                return "Deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting hotel with ID {request.hotelId} from the wish list for client ID {request.clientId}");
                return "An error occurred while processing your request.";
            }
        }
    }
}
