using Booking.Application.Services.ClientProfile;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.ClientProfile.Commands.UpdateProfile;

public class UpdateProfileCommandHandler(ILogger<UpdateProfileCommandHandler> _logger,
    IClientProfileService _clientProfileService) : IRequestHandler<UpdateProfileCommand, string>
{
    public async Task<string> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var response = await _clientProfileService.UpdateAsync(request);
        return response;
    }
}
