using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;

namespace Booking.Infrastructure.Repositories;

public class ReservationRepository(BookingDbContext _context) : IReservationRepository
{
    public async Task<int> CreateAsync(Reservation reservation)
    {            
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
        return reservation.Id;
    }

    public async Task<Reservation> GetByIdAsync(int id)
    {
        return await _context.Reservations.FindAsync(id);
    }
}
