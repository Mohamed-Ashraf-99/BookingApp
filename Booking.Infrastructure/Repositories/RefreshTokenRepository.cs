using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;

namespace Booking.Infrastructure.Repositories;

public class RefreshTokenRepository(BookingDbContext _context) : IRefreshTokenRepository
{
    public async Task AddAsync(UserRefreshToken userRefreshToken)
    {
        await _context.UsersRefreshTokens.AddAsync(userRefreshToken);
        await _context.SaveChangesAsync();
    }
}
