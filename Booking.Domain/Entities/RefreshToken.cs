namespace Booking.Domain.Entities;

public class RefreshToken
{
    public string UserName { get; set; }
    public string Token { get; set; }
    public DateTime ExpireDate { get; set; }
}
