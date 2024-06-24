using FluentValidation;

namespace Booking.Application.Authorization.Commands.EditRole;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator()
    {
        RuleFor(role => role.RoleName)
            .NotEmpty().WithMessage("Role name is Required")
            .NotNull().WithMessage("Role name is Required");

    }
}
