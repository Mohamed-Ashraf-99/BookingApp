﻿using FluentValidation;

namespace Booking.Application.Emails.Commands.SendEmail;

public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is Required")
            .NotNull().WithMessage("Email is Required");

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("Message is Required")
            .NotNull().WithMessage("Message is Required");

        RuleFor(x => x.Subject)
           .NotEmpty().WithMessage("Subject is Required")
           .NotNull().WithMessage("Subject is Required");
    }
}
