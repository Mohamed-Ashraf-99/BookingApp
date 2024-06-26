using FluentValidation;

namespace Booking.Application.Booking.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.")
            .Must(BeAValidDate).WithMessage("Start date must be a valid date.")
            .LessThan(x => x.EndDate).WithMessage("Start date must be before the end date.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .Must(BeAValidDate).WithMessage("End date must be a valid date.");

        RuleFor(x => x.NumberOfGuests)
            .GreaterThan(0).WithMessage("Number of guests must be greater than 0.");

        RuleFor(x => x.ClientId)
            .GreaterThan(0).WithMessage("Client ID is required.");

        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("At least one room must be selected.");

       
    }
    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}
