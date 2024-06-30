

using Booking.Domain.Entities;

namespace Booking.Domain.Repositories;

public interface IOwnerRepository
{
    Task<int> CreateAsync(Owner owner);
    Task<Owner> GetOwnerByUserId(int Id);
    Task<IEnumerable<Owner>> GetAllUnVerifiedOwners();
    Task<Owner> GetOwnerById(int id);
    Task<bool> UpdateOwnerAsync(Owner owner);
    Task<int> GetUserIdByOwnerId(int ownerId);

}
