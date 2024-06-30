using Booking.Domain.Entities;

namespace Booking.Domain.Repositories;

public interface IClientRepository
{
    Task<int> GetClientIdByUserId(int userId);
    Task<int> CreateAsync(Client client);
    Task<bool> DeleteAsync(int userId);
}
