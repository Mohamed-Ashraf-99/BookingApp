using MediatR;

namespace Booking.Application.ApplicationUser.Commands.Register;

public class RegisterUserCommand : IRequest<string>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? PhoneNumber { get; set; }
}
