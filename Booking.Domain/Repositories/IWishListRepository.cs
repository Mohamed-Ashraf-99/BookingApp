using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Repositories
{
    public interface IWishListRepository 
    {
        Task<WishList> GetWishListByClientIdAsync(int clientId);

        Task <int> AddWishListforClient(WishList wishList);

        Task AddHotelsToWishList(HotelWishList hotelwishList);

        Task DeleteUserWishList(HotelWishList hotelWishList);

        Task DeleteWishListAsync(WishList wishList);

        Task UpdateWishListStatusAsync(WishList wishList);
    }
}
