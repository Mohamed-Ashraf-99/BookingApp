namespace Booking.Domain.Repositories;

public interface IClientRepository
{
    Task<int> GetClientIdByUserId(int userId);
}
