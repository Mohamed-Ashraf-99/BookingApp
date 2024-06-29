

using Booking.Domain.Entities;

namespace Booking.Domain.Repositories;

public interface IOwnerRepository
{
    Task<int> CreateAsync(Owner owner);
    Task<Owner> GetOwnerByUserId(int Id);
}
