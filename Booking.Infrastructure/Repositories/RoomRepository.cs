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

    public async Task AddRoomAsync(Room room)
    {
       await _context.Rooms.AddAsync(room);
        _context.SaveChanges();
    }

    public async Task<int> GetRoomCountInHotel(int hotelId)
    {
        return await _context.Rooms
                     .Where(r => r.HotelId == hotelId)
                     .CountAsync();
    }

    public async Task DeleteRoom(int roomId)
    {
        var room = await _context.Rooms.FindAsync(roomId);
        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();   
    }
}
