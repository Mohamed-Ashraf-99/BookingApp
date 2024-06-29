

using FluentValidation;

namespace Booking.Application.Authentication.Queries.ResetPassword;

public class ConfirmPasswordQueryValidator : AbstractValidator<ConfirmResetPasswordQuery>
{
    public ConfirmPasswordQueryValidator()
    {
        RuleFor(x => x.ResetCode)
            .NotEmpty().WithMessage("Reset code is required!")
            .NotNull().WithMessage("Reset code is required!");

        RuleFor(user => user.Email)
           .NotEmpty().WithMessage("Email is required.")
           .EmailAddress().WithMessage("Invalid email format.");
    }
}
