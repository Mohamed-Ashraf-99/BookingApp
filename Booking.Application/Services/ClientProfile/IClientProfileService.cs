using Booking.Application.ClientProfile.Commands.UpdateProfile;

namespace Booking.Application.Services.ClientProfile;

public interface IClientProfileService
{
    Task<string> UpdateAsync(UpdateProfileCommand command);
}
