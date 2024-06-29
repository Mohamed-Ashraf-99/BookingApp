
using Booking.Application.Services.Authentication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authentication.Queries.ResetPassword;

public class ConfirmPasswordQueryHandler(ILogger<ConfirmPasswordQueryHandler> _logger,
    IAuthenticationServices _authenticationServices) : IRequestHandler<ConfirmResetPasswordQuery, string>
{
    public async Task<string> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Reset Password Request");
        var response = await _authenticationServices.ConfirmResetPassword(request.ResetCode, request.Email);
        return response;
    }
}
