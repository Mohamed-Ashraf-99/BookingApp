using Booking.Application.Services.ClientProfile;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.ClientProfile.Commands.UpdateProfile;

public class UpdateProfileCommandHandler(ILogger<UpdateProfileCommandHandler> _logger,
    IClientProfileService _clientProfileService) : IRequestHandler<UpdateProfileCommand, string>
{
    public async Task<string> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateProfileCommand for User ID {UserId}.", request.UserId);

        try
        {
            var response = await _clientProfileService.UpdateAsync(request);
            _logger.LogInformation("UpdateProfileCommand for User ID {UserId} handled successfully.", request.UserId);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling UpdateProfileCommand for User ID {UserId}.", request.UserId);
            return "Internal Server Error. Please try again later.";
        }
    }
}
