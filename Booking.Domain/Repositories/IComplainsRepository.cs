using Booking.Domain.Entities;

namespace Booking.Domain.Repositories;

public interface IComplainsRepository
{
    Task<int> CreateAsync(Complains complains);
    Task<List<Complains>> GetComplainsByHotelIdAsync(int hotelId);
    Task<List<Complains>> GetComplainsByUserIdAsync(int userId);
}
