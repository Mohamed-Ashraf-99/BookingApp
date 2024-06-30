
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.ClientProfile.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<string>
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public IFormFile? Image { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? PhoneNumber { get; set; }
}
