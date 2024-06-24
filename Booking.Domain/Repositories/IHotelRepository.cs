using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Repositories
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetHotelsByCityAsync(string city);
        Task<IEnumerable<Hotel>> GetAllHotelsAsync();

        Task<Hotel> GetHotelByIdAsync(int hotelId);
        Task<IEnumerable< Hotel>> GetHotelByOwnerIdAsync(int ownerId);

        Task<IEnumerable<Hotel>> GetTrendingHotelsAsync();
    }
}
