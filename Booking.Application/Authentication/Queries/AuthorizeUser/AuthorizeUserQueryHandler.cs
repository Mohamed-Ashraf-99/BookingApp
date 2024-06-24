using Booking.Application.Services.Authentication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authentication.Queries.AuthorizeUser;

public class AuthorizeUserQueryHandler(ILogger<AuthorizeUserQueryHandler> _logger,
    IAuthenticationServices _authenticationServices) : IRequestHandler<AuthorizeUserQuery, string>
{
    public async Task<string> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
    {
        var result = await _authenticationServices.ValidateToken(request.AccessToken);
        if (result == "NotExpired")
            return result;
        return "Expired";
    }
}
