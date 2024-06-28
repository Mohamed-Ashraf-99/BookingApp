
using Booking.Application.Emails.Helpers;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Services.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;
    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }
    public async Task<string> SendEmailAsync(string mailTo, string body, string subject, IList<IFormFile> attachments = null)
    {
        try
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_emailSettings.FromEmail),
                Subject = subject,
            };

            email.To.Add(MailboxAddress.Parse(mailTo));

            var builder = new BodyBuilder();

            if (attachments is not null)
            {
                byte[] fileBytes;
                foreach (var attachment in attachments)
                {
                    if (attachment.Length > 0)
                    {
                        using var stream = new MemoryStream();
                        await attachment.CopyToAsync(stream);
                        fileBytes = stream.ToArray();
                        builder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                    }
                }
            }

            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.FromEmail));

            using var client = new SmtpClient();

            // Set timeout to 30 seconds (adjust as necessary)
            client.Timeout = 30000;

            // Attempt to send email with retries
            int retryCount = 3; // Number of retry attempts
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.SslOnConnect);
                    await client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password);
                    await client.SendAsync(email);
                    await client.DisconnectAsync(true);

                    _logger.LogInformation($"Email sent successfully to {mailTo}");
                    return "Email sent successfully.";
                }
                catch (SmtpProtocolException ex)
                {
                    _logger.LogError(ex, $"SMTP Protocol Exception occurred sending email to {mailTo}");
                    // Retry logic: wait before retrying
                    await Task.Delay(2000); // 2 seconds delay before retrying
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error sending email to {mailTo}");
                    return $"Failed to send email: {ex.Message}";
                }
            }

            return "Failed to send email after retries.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending email to {mailTo}");
            return $"Failed to send email: {ex.Message}";
        }
    }

}
