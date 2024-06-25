
using MediatR;

namespace Booking.Application.Authentication.Commands.Logout;

public class LogoutCommand : IRequest<string>
{
    public string RefreshToken { get; set; }
}
