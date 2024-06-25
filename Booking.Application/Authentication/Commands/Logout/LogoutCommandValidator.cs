

using FluentValidation;

namespace Booking.Application.Authentication.Commands.Logout;

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh Token is required!")
            .NotNull().WithMessage("Refresh Token is required!");
    }
}
