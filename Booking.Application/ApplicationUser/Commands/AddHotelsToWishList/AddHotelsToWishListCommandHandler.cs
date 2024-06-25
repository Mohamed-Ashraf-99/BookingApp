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
    IWishListRepository wishListRepository, IHotelRepository hotelRepository) : IRequestHandler<AddHotelsToWishListCommand,string>
    {


        public async Task<string> Handle(AddHotelsToWishListCommand request, CancellationToken cancellationToken)
        {
            var wishList = await wishListRepository.GetWishListByClientIdAsync(request.clientId);
            if (wishList == null)
            {
                wishList = new WishList { ClientId = request.clientId, IsDeleted = false, HotelWishLists = new List<HotelWishList>() };
                await wishListRepository.AddWishListforClient(wishList);
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
                return "Hotel already exists in the wishlist";
            }

            HotelWishList hotelWishList = new HotelWishList { HotelsId = request.HotelId, WishListsId = wishList.Id };
            wishList.HotelWishLists.Add(hotelWishList);
            await wishListRepository.AddHotelsToWishList(hotelWishList);

            return "Insertion succeeded";
        }
    }
}
