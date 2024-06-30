using Microsoft.AspNetCore.Http;

namespace Booking.Application.ClientProfile.Queries.GetClientData;

public class UserDataDto
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string? Image { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? PhoneNumber { get; set; }
}
