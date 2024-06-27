using Booking.Domain.Entities.Identity;

namespace Booking.Domain.Entities;

public class Admin
{
    public int Id { get; set; }
    public virtual User? User { get; set; }
}
