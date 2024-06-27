
using Booking.Application.Emails.Helpers;
using MailKit.Net.Smtp;
using MimeKit;

namespace Booking.Application.Services.Email;

public class EmailService(EmailSettings _emailSettings) : IEmailService
{
    public async Task<string> SendEmailAsync(string email, string message, string subject)
    {

        try
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                var bodybuilder = new BodyBuilder
                {
                    HtmlBody = $"{message}",
                    TextBody = "welcome",
                };
                var mimeMessage = new MimeMessage
                {
                    Body = bodybuilder.ToMessageBody()
                };
                mimeMessage.From.Add(new MailboxAddress("Booking Team", _emailSettings.FromEmail));
                mimeMessage.To.Add(new MailboxAddress("testing", email));
                mimeMessage.Subject = subject == null ? "No Submitted" : subject;
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
            return "Success";
        }
        catch (Exception ex)
        {
            return "Failed";
        }
    }
}
