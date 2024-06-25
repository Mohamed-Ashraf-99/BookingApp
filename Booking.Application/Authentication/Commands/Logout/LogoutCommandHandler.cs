

using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authentication.Commands.Logout;

public class LogoutCommandHandler(ILogger<LogoutCommandHandler> _logger,
    IRefreshTokenRepository _refreshTokenRepository) : IRequestHandler<LogoutCommand, string>
{
    public async Task<string> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Logging out user with RefreshToken: {request.RefreshToken}");

        try
        {
            var response = await _refreshTokenRepository.DeleteAsync(request.RefreshToken);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during logout process");
            throw new Exception(ex.Message, ex);
        }
    }
}
