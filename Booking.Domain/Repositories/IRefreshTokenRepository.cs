﻿using Booking.Domain.Entities;

namespace Booking.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(UserRefreshToken userRefreshToken);
    IQueryable<UserRefreshToken> GetTableNoTracking();
    Task UpdateAsync(UserRefreshToken entity);
    Task<string> DeleteAsync(string refreshToken);
}
