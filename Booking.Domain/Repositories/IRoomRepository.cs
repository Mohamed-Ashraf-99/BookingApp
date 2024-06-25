using Booking.Domain.Entities;

namespace Booking.Domain.Repositories;

public interface IRoomRepository
{
    Task<Room> GetByIdAsync(int id);
}
