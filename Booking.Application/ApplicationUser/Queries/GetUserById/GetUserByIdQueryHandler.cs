using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetUserById.Dto;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Booking.Application.ApplicationUser.Queries.GetUserById;

public class GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> _logger,
    IMapper _mapper,
    UserManager<User> _userManager) : IRequestHandler<GetUserByIdQuery, GetUserByIdDto>
{
    public async Task<GetUserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Retrieving user with ID: {UserId}", request.UserId);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("User with ID: {UserId} not found", request.UserId);
                throw new NotFoundException(nameof(User), request.UserId.ToString());
            }

            var userDto = _mapper.Map<GetUserByIdDto>(user);

            _logger.LogInformation("Successfully retrieved user with ID: {UserId}", request.UserId);

            return userDto;
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "User with ID: {UserId} not found", request.UserId);
            throw new NotFoundException(nameof(User), request.UserId.ToString());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving user with ID: {UserId}", request.UserId);
            throw new ApplicationException($"An unexpected error occurred while retrieving user with ID: {request.UserId}", ex);
        }
    }

}

