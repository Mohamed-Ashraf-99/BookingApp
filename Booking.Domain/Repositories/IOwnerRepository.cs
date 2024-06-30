using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Booking.Domain.Repositories;

namespace Booking.Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task<int> CreateAsync(Owner owner);
        Task<Owner> GetOwnerByUserId(int Id);
        Task<IEnumerable<Owner>> GetAllUnVerifiedOwners();
        Task<Owner> GetOwnerById(int id);
        Task<bool> UpdateOwnerAsync(Owner owner);
        Task<int> GetUserIdByOwnerId(int ownerId);
        Task<bool> DeleteAsync(int ownerId);
        Task<int> GetOwnerIdByUserId(int userId);

        Task<int> AddHotels(Hotel hotel);
        Task AddImagesForHotels(Images image);
        Task UpdateChanges();

    }
}