using FluentValidation;

namespace Booking.Application.Authorization.Commands.DeleteRole;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(role => role.Id)
            .NotEmpty().WithMessage("Id is Required")
            .NotNull().WithMessage("Id is Required");
    }
}
