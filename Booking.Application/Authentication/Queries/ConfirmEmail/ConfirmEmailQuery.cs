using MediatR;

namespace Booking.Application.Authentication.Queries.ConfirmEmail;

public class ConfirmEmailQuery : IRequest<string>
{
    public int UserId { get; set; }
    public string Code { get; set; }
}
