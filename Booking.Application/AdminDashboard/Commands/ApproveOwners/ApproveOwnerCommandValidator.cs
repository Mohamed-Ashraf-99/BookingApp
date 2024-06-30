
using FluentValidation;

namespace Booking.Application.AdminDashboard.Commands.ApproveOwners;

public class ApproveOwnerCommandValidator : AbstractValidator<ApproveOwnerCommand>
{
    public ApproveOwnerCommandValidator()
    {
        RuleFor(x=>x.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotNull().WithMessage("Id is required");
    }
}
