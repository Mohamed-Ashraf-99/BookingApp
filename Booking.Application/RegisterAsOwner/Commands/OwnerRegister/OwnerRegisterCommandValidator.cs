using FluentValidation;

namespace Booking.Application.RegisterAsOwner.Commands.OwnerRegister
{
    public class OwnerRegisterCommandValidator : AbstractValidator<OwnerRegisterCommand>
    {
        public OwnerRegisterCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address format.")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.City)
                .MaximumLength(100).WithMessage("City name must not exceed 100 characters.");

            RuleFor(x => x.Street)
                .MaximumLength(100).WithMessage("Street name must not exceed 100 characters.");

            RuleFor(x => x.PostalCode)
                .MaximumLength(20).WithMessage("Postal code must not exceed 20 characters.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.");

            RuleFor(x => x.Certificate)
                .Must(file => file != null && file.Length > 0).WithMessage("Attachment file is required.");
           
        }
    }
}
