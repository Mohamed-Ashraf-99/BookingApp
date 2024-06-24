using MediatR;

namespace Booking.Application.Authentication.Queries.AuthorizeUser;

public class AuthorizeUserQuery : IRequest<string>
{
    public string AccessToken { get; set; }

}
