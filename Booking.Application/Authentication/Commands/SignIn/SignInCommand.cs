using Booking.Application.Authentication.Helpers;
using MediatR;

namespace Booking.Application.Authentication.Commands.SignIn;

public class SignInCommand : IRequest<JwtAuthResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
