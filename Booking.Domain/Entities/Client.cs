using Booking.Domain.Entities.Identity;

namespace Booking.Domain.Entities;

public class Client
{
    public int Id { get; set; }
    public virtual User? User { get; set; }
    public Address? Address { get; set; }
    public virtual List<Complains>? Complains { get; set; }
    public virtual WishList? WishList { get; set; }
    public virtual List<Reviews>? Reviews { get; set; }
    public virtual List<Reservation>? Reservaions { get; set; }
}
