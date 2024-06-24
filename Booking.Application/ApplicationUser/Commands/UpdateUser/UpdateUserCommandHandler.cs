using AutoMapper;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Booking.Application.ApplicationUser.Commands.UpdateUser;

public class UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> _logger,
    UserManager<User> _userManager,
    IMapper _mapper) : IRequestHandler<UpdateUserCommand, string>
{
    public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Logging the start of the operation
        _logger.LogInformation("Updating user with ID: {UserId}", request.Id);

        // Check if the user exists
        var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
        if (oldUser is null)
        {
            _logger.LogWarning("User with ID : {UserId} not found", request.Id);
            throw new NotFoundException(nameof(User), request.Id.ToString());
        }

        // Mapping the updated fields
        _mapper.Map(request, oldUser);

        // Updating the user
        var result = await _userManager.UpdateAsync(oldUser);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("Update failed for user with ID : {UserId}. Errors: {Errors}", request.Id, errors);
            return $"Update failed. Errors: {errors}";
        }

        // Logging the successful update
        _logger.LogInformation("User with ID: {UserId} updated successfully", request.Id);
        return $"Update for user with ID: {request.Id} succeeded";

    }
}
