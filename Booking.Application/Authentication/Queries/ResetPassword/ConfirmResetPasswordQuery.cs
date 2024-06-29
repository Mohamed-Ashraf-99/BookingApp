
using MediatR;

namespace Booking.Application.Authentication.Queries.ResetPassword;

public class ConfirmResetPasswordQuery : IRequest<string>
{
    public string ResetCode { get; set; }
    public string Email { get; set; }
}
