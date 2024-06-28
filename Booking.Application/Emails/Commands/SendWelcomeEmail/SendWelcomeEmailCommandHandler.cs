
using Booking.Application.Emails.Commands.SendEmail;
using Booking.Application.Services.Email;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Emails.Commands.SendWelcomeEmail;

public class SendWelcomeEmailCommandHandler(ILogger<SendEmailCommandHandler> _logger,
    IEmailService _emailService) : IRequestHandler<SendWelcomeEmailCommand, string>
{
    public async Task<string> Handle(SendWelcomeEmailCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{request.Email} Sending Email at {DateTime.Now}");
        var filePath = $"{Directory.GetCurrentDirectory()}\\Template\\ConfirmEmailTemplate.html";


        var str = new StreamReader(filePath);

        var mailText = str.ReadToEnd();
        str.Close();

        mailText = mailText.Replace("[username]", request.UserName).Replace("[email]", request.Email);
        var response = await _emailService.SendEmailAsync(request.Email, mailText, "Welcome to our website");
        return response;
    }
}
