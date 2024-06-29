

using MediatR;

namespace Booking.Application.Authentication.Commands.UpdatePassword;

public class ResetPasswordCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
