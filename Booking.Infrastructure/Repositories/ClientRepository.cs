using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class ClientRepository(BookingDbContext _context) : IClientRepository
{
    public async Task<int> GetClientIdByUserId(int userId)
    {
        var client = await _context.Clients.Include(x => x.User).Where(usr => usr.User.Id == userId).FirstOrDefaultAsync();
        var clientId = client.Id;
        return clientId;
    }
}
