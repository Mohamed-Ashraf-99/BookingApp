
using MediatR;

namespace Booking.Application.Emails.Commands.SendWelcomeEmail;

public class SendWelcomeEmailCommand : IRequest<string>
{
    public string UserName { get; set; }
    public string Email { get; set; }
}
