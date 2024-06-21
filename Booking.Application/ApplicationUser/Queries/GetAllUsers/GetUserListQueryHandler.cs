using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllUsers.Dto;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Logging;

namespace Booking.Application.ApplicationUser.Queries.GetAllUsers;

public class GetUserListQueryHandler(ILogger<GetUserListQueryHandler> _logger,
    IMapper _mapper,
    UserManager<User> _userManager) : IRequestHandler<GetUserListQuery, IEnumerable<GetUserListDto>>
{
    public async Task<IEnumerable<GetUserListDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Retrieving all users data");

            var users = await _userManager.Users.ToListAsync(cancellationToken);

            if (users is null || !users.Any())
            {
                _logger.LogWarning("No users found");
                throw new NotFoundException(nameof(users));
            }

            var userList = _mapper.Map<IEnumerable<GetUserListDto>>(users);

            _logger.LogInformation($"Successfully retrieved {userList.Count()} users");

            return userList;
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "Users not found");
            throw new NotFoundException(nameof(User)); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving users");
            throw new ApplicationException("An unexpected error occurred while retrieving users", ex);
        }

    }
}
