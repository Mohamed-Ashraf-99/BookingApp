using Booking.Application.Authentication.Helpers;
using MediatR;

namespace Booking.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<JwtAuthResult>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
