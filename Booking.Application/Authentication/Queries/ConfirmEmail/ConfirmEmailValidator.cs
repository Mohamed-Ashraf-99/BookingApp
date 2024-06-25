using FluentValidation;

namespace Booking.Application.Authentication.Queries.ConfirmEmail;

public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
{
    public ConfirmEmailValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id required")
            .NotNull().WithMessage("User Id required");

        RuleFor(x => x.Code)
          .NotEmpty().WithMessage("Code Id required")
          .NotNull().WithMessage("Code Id required");
    }
}
