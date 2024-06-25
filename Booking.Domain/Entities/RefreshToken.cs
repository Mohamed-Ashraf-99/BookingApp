namespace Booking.Domain.Entities;

public class RefreshToken
{
    public int UserId { get; set; }
    public List<string> UserRoles { get; set; }
    public string UserName { get; set; }
    public string Token { get; set; }
    public DateTime ExpireDate { get; set; }
}
