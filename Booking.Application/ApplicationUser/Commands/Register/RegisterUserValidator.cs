using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.Register
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            // UserName validation
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

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

            // City validation 
            RuleFor(user => user.City)
                .MaximumLength(100).WithMessage("City name must not exceed 100 characters.")
                .When(user => !string.IsNullOrEmpty(user.City));

            // Street validation 
            RuleFor(user => user.Street)
                .MaximumLength(100).WithMessage("Street name must not exceed 100 characters.")
                .When(user => !string.IsNullOrEmpty(user.Street));

            // Postal Code validation
            RuleFor(user => user.PostalCode)
                .Matches("^[0-9]{5}(?:-[0-9]{4})?$").WithMessage("Invalid postal code format.")
                .When(user => !string.IsNullOrEmpty(user.PostalCode));

            // Phone Number validation 
            RuleFor(user => user.PhoneNumber)
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.")
                .When(user => !string.IsNullOrEmpty(user.PhoneNumber));

        }
    }
}
