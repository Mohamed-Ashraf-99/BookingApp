using Booking.Domain.Entities;

namespace Booking.Domain.Repositories;

public interface IReservationRepository
{
    Task<int> CreateAsync(Reservation reservation);
    Task<Reservation> GetByIdAsync(int id);
}
