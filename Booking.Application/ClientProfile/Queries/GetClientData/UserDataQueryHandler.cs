using AutoMapper;
using Booking.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Booking.Application.ClientProfile.Queries.GetClientData;

public class UserDataQueryHandler(ILogger<UserDataQueryHandler> _logger,
    UserManager<User> _userManager, IMapper _mapper) : IRequestHandler<UserDataQuery, UserDataDto>
{
    public async Task<UserDataDto> Handle(UserDataQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling UserDataQuery for User ID {UserId}.", request.Id);

            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                var message = $"User with ID {request.Id} not found.";
                _logger.LogWarning(message);
                throw new KeyNotFoundException(message);
            }

            var userDto = _mapper.Map<UserDataDto>(user);

            _logger.LogInformation("Successfully handled UserDataQuery for User ID {UserId}.", request.Id);
            return userDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling UserDataQuery for User ID {UserId}.", request.Id);
            throw new ApplicationException("An error occurred while retrieving user data.", ex);
        }
    }
}
