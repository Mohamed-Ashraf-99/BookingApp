
using Booking.Application.Services.Authentication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authentication.Commands.UpdatePassword;

public class ResetPasswordCommandHandler(ILogger<ResetPasswordCommandHandler> _logger,
    IAuthenticationServices _authenticationServices) : IRequestHandler<ResetPasswordCommand, string>
{
    public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Reset password to user with email {request.Email}");
        var response = await _authenticationServices.ResetPassword(request.Email, request.Password);
        return response;
    }
}
