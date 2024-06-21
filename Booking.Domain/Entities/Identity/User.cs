using Microsoft.AspNetCore.Identity;

namespace Booking.Domain.Entities.Identity;

public class User : IdentityUser<int>
{
    public Address? Address { get; set; }
}
