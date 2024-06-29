using Booking.Domain.Entities;

namespace Booking.Domain.Repositories;

public interface IRoomRepository
{
    Task<Room> GetByIdAsync(int id);

    Task AddRoomAsync(Room room);

    Task<int> GetRoomCountInHotel(int hotelId);
}
