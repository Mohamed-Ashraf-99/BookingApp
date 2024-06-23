using Booking.Application.Services.Authorization;
using FluentValidation;

namespace Booking.Application.Authorization.Commands;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IAuthorizationServices _authorizationServices;
    public  CreateRoleCommandValidator(IAuthorizationServices authorizationServices)
    {
        _authorizationServices = authorizationServices;

        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Role name is required!")
            .NotNull().WithMessage("Role name is required!");

        
    }
}
