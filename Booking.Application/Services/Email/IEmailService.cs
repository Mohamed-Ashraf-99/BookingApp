using Microsoft.AspNetCore.Http;

namespace Booking.Application.Services.Email;

public interface IEmailService
{
    Task<string> SendEmailAsync(string mailTo, string body, string subject, IList<IFormFile> attachments = null);
}
