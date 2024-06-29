using System;
using System.Linq;
using System.Threading.Tasks;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class OwnerRepository : IOwnerRepository
{
    private readonly BookingDbContext _context;
    private readonly ILogger<OwnerRepository> _logger;

    public OwnerRepository(BookingDbContext context, ILogger<OwnerRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> CreateAsync(Owner owner)
    {
        try
        {
            await _context.Owner.AddAsync(owner);
            await _context.SaveChangesAsync();
            return owner.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating an owner.");
            throw new ApplicationException("An error occurred while creating an owner.", ex);
        }
    }

    public async Task<Owner> GetOwnerByUserId(int userId)
    {
        try
        {
            var owner = await _context.Owner
                .Include(x => x.User)
                .FirstOrDefaultAsync(usr => usr.User.Id == userId);

            if (owner == null)
            {
                _logger.LogWarning($"No owner found for user id {userId}");
            }

            return owner;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while retrieving the owner with user id {userId}");
            throw new ApplicationException($"An error occurred while retrieving the owner with user id {userId}", ex);
        }
    }
}
