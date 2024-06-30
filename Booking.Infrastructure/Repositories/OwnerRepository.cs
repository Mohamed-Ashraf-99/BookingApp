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

    public async Task<IEnumerable<Owner>> GetAllUnVerifiedOwners()
    {
        try
        {
            var unVerifiedOwners = await _context.Owner.Include(x => x.User).Where(x => x.IsVerified == false).ToListAsync();
            return unVerifiedOwners;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving un verified owners.");
            throw new ApplicationException("An error occurred while retrieving un verified owners.", ex);
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

    public async Task<Owner> GetOwnerById(int id)
    {
        try
        {
            var owner = await _context.Owner
                .Include(x => x.User)
                .FirstOrDefaultAsync(usr => usr.Id == id);

            if (owner == null)
            {
                _logger.LogWarning($"No owner found for user id {id}");
            }

            return owner;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while retrieving the owner with user id {id}");
            throw new ApplicationException($"An error occurred while retrieving the owner with user id {id}", ex);
        }
    }

    public async Task<bool> UpdateOwnerAsync(Owner owner)
    {
        try
        {
            _context.Owner.Update(owner);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"An error occurred while updating the owner with user id {owner.Id}");
            throw new Exception("Error updating owner in database", ex);
        }
    }

    public async Task<int> GetUserIdByOwnerId(int ownerId)
    {
        var owner = await GetOwnerById(ownerId);
        var userId = owner.User.Id;
        return userId;
    }

    public async Task<bool> DeleteAsync(int ownerId)
    {
        var owner = await GetOwnerById(ownerId);
        try
        {
            _context.Owner.Remove(owner);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the owner with user id {owner.Id}");
            throw new Exception("Error deleting owner in database", ex);
        }
    }
}
