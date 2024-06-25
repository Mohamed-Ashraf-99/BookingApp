using MediatR;

namespace Booking.Application.Emails.Commands.SendEmail;

public class SendEmailCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Message { get; set; }
    public string Subject { get; set; }
}
