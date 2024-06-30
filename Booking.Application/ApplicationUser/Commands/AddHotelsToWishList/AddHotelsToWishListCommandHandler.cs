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

namespace Booking.Application.ApplicationUser.Commands.AddHotelsToWishList
{
    public class AddHotelsToWishListCommandHandler(ILogger<AddHotelsToWishListCommandHandler> _logger,
    IWishListRepository wishListRepository, IHotelRepository hotelRepository,IClientRepository clientRepository) : IRequestHandler<AddHotelsToWishListCommand,string>
    {


        public async Task<string> Handle(AddHotelsToWishListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.UserId = await clientRepository.GetClientIdByUserId(request.UserId);
                var wishList = await wishListRepository.GetWishListByClientIdAsync(request.UserId);

                if (wishList == null)
                {
                    _logger.LogInformation($"Creating a new wishlist for client with ID {request. UserId}");
                    wishList = new WishList { ClientId = request.UserId, IsDeleted = false, HotelWishLists = new List<HotelWishList>() };
                    await wishListRepository.AddWishListforClient(wishList);
                }
                else if (wishList.IsDeleted == true)
                {
                    _logger.LogInformation($"Wishlist for client with ID {request.UserId} is marked as deleted. Updating IsDeleted to false.");
                    wishList.IsDeleted = false;
                    await wishListRepository.UpdateWishListStatusAsync(wishList);
                }

                var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId);
                if (hotel == null)
                {
                    _logger.LogError($"Hotel with ID {request.HotelId} not found.");
                    throw new ApplicationException($"Hotel with ID {request.HotelId} not found.");
                }

                if (wishList.HotelWishLists.Any(h => h.HotelsId == request.HotelId))
                {
                    _logger.LogInformation($"Hotel with ID {request.HotelId} is already in the wishlist.");
                    return "Hotel already exists in the wishlist.";
                }

                var hotelWishList = new HotelWishList { HotelsId = request.HotelId, WishListsId = wishList.Id };
                wishList.HotelWishLists.Add(hotelWishList);
                await wishListRepository.AddHotelsToWishList(hotelWishList);

                _logger.LogInformation($"Hotel with ID {request.HotelId} successfully added to the wishlist for client ID {request.UserId}.");
                return "Insertion succeeded.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding hotel with ID {request.HotelId} to the wishlist for client ID {request.HotelId}.");
                return "An error occurred while processing your request.";
            }
        }
    }
}
