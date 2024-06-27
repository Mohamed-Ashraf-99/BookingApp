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
    public class ReviewsRepository(BookingDbContext _context) : IReviewsRepository
    {
        public async Task<IEnumerable<Reviews>> GetAllReviewsAync()
        {
           return await _context.Reviews.Include(r=>r.Hotel).Include(r=>r.Client).ThenInclude(r=>r.User).ToListAsync();
        }

        public async Task<decimal?> GetTotalAverageReviews()
        {
            return await _context.Reviews.AverageAsync(r => r.Rate);
           
        }

        public async Task InsertReview(Reviews review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
    }
}
