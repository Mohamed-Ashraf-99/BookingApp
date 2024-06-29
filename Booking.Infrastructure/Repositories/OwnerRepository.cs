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
    public class OwnerRepository (BookingDbContext _context) : IOwnerRepository
    {
        public async Task<int> AddHotels(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();

            return hotel.Id;
        }

        public async Task AddImagesForHotels(Images image)
        {
            await _context.images.AddAsync(image);
            //await _context.SaveChangesAsync();
        }

        public async Task<int> GetOwnerIdByUserId(int userId)
        {
            var owner = await _context.Owner.Include(x => x.User).Where(usr => usr.User.Id == userId).FirstOrDefaultAsync();
            var ownerId = owner.Id;
            return ownerId;
        }

        public async Task UpdateChanges()
        {
             _context.SaveChanges();
        }
    }
}
