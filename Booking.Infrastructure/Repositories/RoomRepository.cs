using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class RoomRepository(BookingDbContext _context) : IRoomRepository
{
    public async Task<Room> GetByIdAsync(int id)
    {
        return await _context.Rooms.FindAsync(id);
    }
}
