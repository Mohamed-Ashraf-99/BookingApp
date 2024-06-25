namespace Booking.Application.Services.Email;

public interface IEmailService
{
    Task<string> SendEmailAsync(string email, string message, string subject);
}
