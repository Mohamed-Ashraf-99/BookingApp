using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Emails.Commands.SendEmail;

public class SendEmailCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Body { get; set; }
    public string Subject { get; set; }
    public IList<IFormFile> Attachments { get; set; } = new List<IFormFile>();
}
