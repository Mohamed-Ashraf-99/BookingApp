using Booking.Application.Services.Email;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Emails.Commands.SendEmail;

public class SendEmailCommandHandler(ILogger<SendEmailCommandHandler> _logger,
    IEmailService _emailService) : IRequestHandler<SendEmailCommand, string>
{
    public async Task<string> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{request.Email} Sending Email at {DateTime.Now}");
        var response = await _emailService.SendEmailAsync(request.Email,request.Message, request.Subject);
        return response;
    }
}
