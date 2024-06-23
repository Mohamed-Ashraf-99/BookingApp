using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Repositories
{
    public interface IReviewsRepository
    {
        Task<decimal?> GetTotalAverageReviews();
        Task<IEnumerable<Reviews>> GetAllReviewsAync();
    }
}
