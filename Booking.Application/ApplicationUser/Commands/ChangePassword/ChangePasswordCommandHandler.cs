using Booking.Domain.Entities.Identity;
using Booking.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Booking.Application.ApplicationUser.Commands.ChangePassword;

public class ChangePasswordCommandHandler(ILogger<ChangePasswordCommandHandler> _logger,
    UserManager<User> _userManager) : IRequestHandler<ChangePasswordCommand, string>
{
    public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Changing password for user with ID: {UserId}", request.Id);

            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                _logger.LogWarning("User with ID: {UserId} not found", request.Id);
                throw new NotFoundException(nameof(User), request.Id.ToString());
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Password change failed for user with ID: {UserId}. Errors: {Errors}", request.Id, errors);
                return $"Password change failed. Errors: {errors}";
            }

            _logger.LogInformation("Password changed successfully for user with ID: {UserId}", request.Id);
            return "Password changed successfully";
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogWarning(ex, "Task was canceled while changing password for user with ID: {UserId}", request.Id);
            return "The operation was canceled.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while changing password for user with ID: {UserId}", request.Id);
            return "An unexpected error occurred";
        }
    }
}

