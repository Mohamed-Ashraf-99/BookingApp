using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class ClientRepository(BookingDbContext _context) : IClientRepository
{
    public async Task<int> CreateAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return client.Id;
    }

    public async Task<bool> DeleteAsync(int userId)
    {
        var client = await _context.Clients.Include(x => x.User).Where(usr => usr.User.Id == userId).FirstOrDefaultAsync();
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetClientIdByUserId(int userId)
    {
        var client = await _context.Clients.Include(x => x.User).Where(usr => usr.User.Id == userId).FirstOrDefaultAsync();
        var clientId = client.Id;
        return clientId;
    }

  
}
