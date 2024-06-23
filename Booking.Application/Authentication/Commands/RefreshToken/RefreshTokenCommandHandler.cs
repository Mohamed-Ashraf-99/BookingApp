using Booking.Application.Authentication.Helpers;
using Booking.Application.Services.Authentication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler(ILogger<RefreshTokenCommandHandler> _logger,
    IAuthenticationServices _authenticationServices) : IRequestHandler<RefreshTokenCommand, JwtAuthResult>
{
    public async Task<JwtAuthResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Refresh token creation");
        var result = await _authenticationServices.GetRefreshToken(request.AccessToken, request.RefreshToken);
        return result;
    }
}
