using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Repositories
{
    public interface IOfferRepository
    {
        Task<IEnumerable<Offer>> GetAllOffersAsync();

        Task<IEnumerable<Offer>> GetOffersByHotelIdAsync(int Id);
    }
}
