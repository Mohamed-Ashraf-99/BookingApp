using Booking.Application.Services.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authentication.Commands.ConformResetPassword;

public class SendPasswordCommandHandler(ILogger<SendPasswordCommandHandler> _logger, IAuthenticationServices _authenticationService) : IRequestHandler<SendResetPasswordCommand, string>
{
    public async Task<string> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Reset password request to user with email {request.Email}");
        var response = await _authenticationService.SendResetPasswordCode(request.Email);
        return response;
    }
}
