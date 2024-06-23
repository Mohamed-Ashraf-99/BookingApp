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
        public async Task<WishList> GetWishListByClientIdAsync(int clientId)
        {
            return await _context.WishList.Include(wl => wl.Hotels).FirstOrDefaultAsync(wl => wl.ClientId == clientId && wl.IsDeleted != true); 
        }
    }
}
