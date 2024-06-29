using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task<int> GetOwnerIdByUserId(int userId);

        Task<int> AddHotels(Hotel hotel);
        Task AddImagesForHotels(Images image);
        Task UpdateChanges();

    }
}
