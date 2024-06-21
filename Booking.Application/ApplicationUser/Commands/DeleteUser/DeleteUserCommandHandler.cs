
using Booking.Domain.Entities.Identity;
using Booking.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Booking.Application.ApplicationUser.Commands.DeleteUser;

public class DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> _logger,
    UserManager<User> _userManager) : IRequestHandler<DeleteUserCommand, string>
{
    public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Deleting user with ID: {UserId}", request.UserId);

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                _logger.LogWarning("User with ID: {UserId} not found", request.UserId);
                throw new NotFoundException(nameof(User), request.UserId.ToString());
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Failed to delete user with ID: {UserId}. Errors: {Errors}", request.UserId, errors);
                return $"Failed to delete user with ID: {request.UserId}. Errors: {errors}";
            }

            _logger.LogInformation("User with ID: {UserId} deleted successfully", request.UserId);
            return $"User with ID: {request.UserId} deleted successfully";
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "User with ID: {UserId} not found", request.UserId);
            return ex.Message;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while deleting user with ID: {UserId}", request.UserId);
            return $"An unexpected error occurred while deleting user with ID: {request.UserId}";
        }
    }
}
