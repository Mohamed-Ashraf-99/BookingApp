using Booking.Application.Services.Authentication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authentication.Queries.ConfirmEmail;

public class ConfirmEmailQueryHandler(ILogger<ConfirmEmailQueryHandler> _logger,
             IAuthenticationServices _authenticationServices) : IRequestHandler<ConfirmEmailQuery, string>
{
    public async Task<string> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
    {
        var result = await _authenticationServices.ConfirmEmail(request.UserId, request.Code);
        return result;
    }
}
