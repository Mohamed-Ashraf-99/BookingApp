namespace Booking.Application.Authentication.Helpers;

public class UserClaimsForLogin
{
    public int UserId { get; set; }
    public List<string> UserRoles { get; set; }
}
