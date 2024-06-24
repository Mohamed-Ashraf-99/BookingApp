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
    public class HotelRepository(BookingDbContext _context, IReviewsRepository reviewsRepo) : IHotelRepository
    {
        public async Task<IEnumerable<Hotel>> GetHotelsByCityAsync(string city)
        {
            return await _context.Hotels
                .Include(h => h.Images).Include(h=>h.Reviews)
                .Where(h => h.Address.City.ToLower() == city.ToLower() && h.IsDeleted!=true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
        {
            return await _context.Hotels
                .Include(h => h.Images).Include(h => h.Reviews).Include(h=>h.Restaurants)
                .Include(h=>h.Owner).Include(h=>h.Complains).Include(h=>h.Rooms).Include(h=>h.Offers)
                .Where(h => h.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<Hotel> GetHotelByIdAsync(int hotelId)
        {
            var hotel = await _context.Hotels
            .Include(h => h.Images)
            .Include(h => h.Reviews)
            .Include(h => h.Restaurants)
            .Include(h => h.Owner)
            .Include(h => h.Complains)
            .Include(h => h.Rooms)
                .ThenInclude(h => h.Packages)
                .ThenInclude(h => h.RoomFacilities)
            .Include(h => h.Rooms)
                .ThenInclude(r => r.Packages)
                .ThenInclude(p => p.Meals)
            .Include(h => h.Rooms)
            .ThenInclude(r => r.Packages)
            .ThenInclude(p => p.PackageFacilities)
            .Include(h => h.Offers)
            .FirstOrDefaultAsync(h => h.Id == hotelId && h.IsDeleted != true);
            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetHotelByOwnerIdAsync(int ownerId)
        {
            return await _context.Hotels
                 .Include(h => h.Images).Include(h => h.Reviews).Include(h => h.Restaurants)
                 .Include(h => h.Owner).Include(h => h.Complains).Include(h => h.Rooms).Include(h => h.Offers)
                 .Where(h => h.OwnerId==ownerId && h.IsDeleted != true)
                 .ToListAsync();
        }
        public async Task<IEnumerable<Hotel>> GetTrendingHotelsAsync()
        {
            // Calculate the overall average rating
            var overallAverageRating = await reviewsRepo.GetTotalAverageReviews();

            // Get hotels with an average rating greater than the overall average rating
            var trendingHotels = await _context.Hotels
                .Include(h => h.Images)
                .Include(h => h.Reviews)
                //.Include(h => h.Restaurants)
                .Include(h => h.Owner)
                //.Include(h => h.Complains)
                //.Include(h => h.Rooms)
                //.Include(h => h.Offers)
                .Where(h => h.IsDeleted != true && h.Reviews.Average(r => r.Rate) > overallAverageRating)
                .ToListAsync();

            return trendingHotels;
        }

    }
}
