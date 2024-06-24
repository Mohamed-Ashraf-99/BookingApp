using Booking.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Domain.Entities;

public class UserRefreshToken
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? RefreshToken { get; set; }
    public string? Token { get; set; }
    public string? JwtId { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpireDate { get; set; }
    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(User.UserRefreshTokens))]
    public virtual User? user { get; set; }
}
