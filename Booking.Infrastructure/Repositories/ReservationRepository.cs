using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class ReservationRepository(BookingDbContext _context) : IReservationRepository
{
    public async Task<int> CreateAsync(Reservation reservation)
    {            
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
        return reservation.Id;
    }

    public async Task DeleteExpiredReservationsAsync()
    {
        var expiredReservations = await _context.Reservations
                 .Where(r => r.EndDate <= DateTime.Now)
                 .ToListAsync();

        _context.Reservations.RemoveRange(expiredReservations);
        await _context.SaveChangesAsync();
    }

    public async Task<Reservation> GetByIdAsync(int id)
    {
        return await _context.Reservations.FindAsync(id);
    }

    public async Task<IEnumerable<Reservation>> GetClientReservationByUserIdAsync(int userId)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(x => x.User.Id == userId);
        var clientReservations = await _context.Reservations.Where(x => x.ClientId == client.Id).ToListAsync();
        return clientReservations;
    }
}
