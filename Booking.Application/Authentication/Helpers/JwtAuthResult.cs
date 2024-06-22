using Booking.Domain.Entities;

namespace Booking.Application.Authentication.Helpers;

public class JwtAuthResult
{
    public string AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}
