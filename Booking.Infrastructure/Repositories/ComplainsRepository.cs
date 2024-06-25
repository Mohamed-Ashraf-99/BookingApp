using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class ComplainsRepository(BookingDbContext _context) : IComplainsRepository
{
    public async Task<int> CreateAsync(Complains complains)
    {
        await _context.Complains.AddAsync(complains);
        await _context.SaveChangesAsync();
        return complains.Id;
    }

    public async Task<List<Complains>> GetComplainsByHotelIdAsync(int hotelId)
    {
        return await _context.Complains
            .Where(c => c.HotelId == hotelId)
            .ToListAsync();
    }

    public async Task<List<Complains>> GetComplainsByUserIdAsync(int userId)
    {
        return await _context.Complains
            .Where(c => c.ClientId == userId)
            .ToListAsync();
    }
}
