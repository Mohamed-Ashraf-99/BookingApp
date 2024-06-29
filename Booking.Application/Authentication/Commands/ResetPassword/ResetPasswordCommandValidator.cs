
using FluentValidation;

namespace Booking.Application.Authentication.Commands.UpdatePassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        // Email validation
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        // Password validation
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        // Confirm Password validation
        RuleFor(user => user.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(user => user.Password).WithMessage("Passwords do not match.");
    }
}
