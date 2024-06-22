using MediatR;

namespace Booking.Application.Authentication.Commands;

public class SignInCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
