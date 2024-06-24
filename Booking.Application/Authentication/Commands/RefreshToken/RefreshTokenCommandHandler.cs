using Booking.Application.Authentication.Helpers;
using Booking.Application.Services.Authentication;
using Booking.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler(ILogger<RefreshTokenCommandHandler> _logger,
    IAuthenticationServices _authenticationServices,
    UserManager<User> _userManager) : IRequestHandler<RefreshTokenCommand, JwtAuthResult>
{
    public async Task<JwtAuthResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Refresh token creation");
        var jwtToken = _authenticationServices.ReadJWTToken(request.AccessToken);
        var userIdAndExpireDate = await _authenticationServices.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
        var (userId, expiryDate) = userIdAndExpireDate;
        var user = await _userManager.FindByIdAsync(userId);
        var result = await _authenticationServices.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
        return result;
    }
}
