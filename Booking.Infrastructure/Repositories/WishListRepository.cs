using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class WishListRepository(BookingDbContext _context) : IWishListRepository
    {
        public async Task AddHotelsToWishList(HotelWishList hotelwishList)
        {
            await _context.HotelWishLists.AddAsync(hotelwishList);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddWishListforClient(WishList wishList)
        {
            await _context.WishList.AddAsync(wishList); 
            await _context.SaveChangesAsync();
            return wishList.Id; 
        }

        public async Task DeleteUserWishList(HotelWishList hotelWishList)
        {
             _context.HotelWishLists.Remove(hotelWishList);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteWishListAsync(WishList wishList)
        {
            wishList.IsDeleted = true;
            UpdateWishListStatusAsync(wishList);
        }
        public async Task<WishList> GetWishListByClientIdAsync(int clientId)
        {
            return await _context.WishList
                .Include(w => w.HotelWishLists)
                   .ThenInclude(hw => hw.Hotel)
                   .ThenInclude(hw=>hw.Images)
                .Include(w => w.HotelWishLists)
                   .ThenInclude(hw => hw.Hotel)
                   .ThenInclude(hw => hw.Reviews)
                .Include(w => w.HotelWishLists)
                   .ThenInclude(hw => hw.Hotel)
                   .ThenInclude(hw => hw.Owner)
                .Include(hw=>hw.Client)
                .FirstOrDefaultAsync(w => w.ClientId == clientId && w.IsDeleted!=true);
        }

        public async Task UpdateWishListStatusAsync(WishList wishList)
        {
            _context.WishList.Update(wishList);
            await _context.SaveChangesAsync();
        }
    }
}
