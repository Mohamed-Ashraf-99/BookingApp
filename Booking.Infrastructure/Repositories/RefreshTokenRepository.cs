using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class RefreshTokenRepository(BookingDbContext _context) : IRefreshTokenRepository
{
    public async Task AddAsync(UserRefreshToken userRefreshToken)
    {
        await _context.UsersRefreshTokens.AddAsync(userRefreshToken);
        await _context.SaveChangesAsync();
    }
    public IQueryable<UserRefreshToken> GetTableNoTracking()
    {
        return _context.UsersRefreshTokens.AsNoTracking().AsQueryable();
    }

    public async Task UpdateAsync(UserRefreshToken entity)
    {
        _context.UsersRefreshTokens.Update(entity);
        await _context.SaveChangesAsync();
    }
}
