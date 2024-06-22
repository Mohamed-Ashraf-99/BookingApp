using Booking.Domain.Entities;

namespace Booking.Domain.Repositories;

public interface IRefreshTokenRepository 
{
    Task AddAsync(UserRefreshToken userRefreshToken);
}
