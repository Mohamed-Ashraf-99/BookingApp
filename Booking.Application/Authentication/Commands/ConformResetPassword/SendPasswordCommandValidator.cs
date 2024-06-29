using FluentValidation;
using MediatR;

namespace Booking.Application.Authentication.Commands.ConformResetPassword;

public class SendPasswordCommandValidator : AbstractValidator<SendResetPasswordCommand>
{
    public SendPasswordCommandValidator()
    {
        RuleFor(user => user.Email)
              .NotEmpty().WithMessage("Email is required.")
              .EmailAddress().WithMessage("Invalid email format.");
    }
}
