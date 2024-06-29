using MediatR;

namespace Booking.Application.Authentication.Commands.ConformResetPassword;

public class SendResetPasswordCommand : IRequest<string>
{
    public string Email { get; set; }
}
