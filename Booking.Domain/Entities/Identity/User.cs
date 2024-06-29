using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Domain.Entities.Identity;

public class User : IdentityUser<int>
{

    public Address? Address { get; set; }
    [InverseProperty(nameof(UserRefreshToken.user))]
    public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }

    public string? Code { get; set; }
    public string? Certificate { get; set; } 
    public string? Image { get; set; }
    public User()
    {
        UserRefreshTokens = new HashSet<UserRefreshToken>();
    }
}
