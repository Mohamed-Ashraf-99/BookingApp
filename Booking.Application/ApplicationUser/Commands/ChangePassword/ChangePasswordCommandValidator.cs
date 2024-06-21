
using FluentValidation;

namespace Booking.Application.ApplicationUser.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        // Current Password validation
        RuleFor(command => command.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required.");

        // New Password validation
        RuleFor(command => command.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(6).WithMessage("New password must be at least 6 characters long.")
            .Matches("[A-Z]").WithMessage("New password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("New password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("New password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("New password must contain at least one special character.");

        // Confirm Password validation
        RuleFor(command => command.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(command => command.NewPassword).WithMessage("Passwords do not match.");

    }
}
