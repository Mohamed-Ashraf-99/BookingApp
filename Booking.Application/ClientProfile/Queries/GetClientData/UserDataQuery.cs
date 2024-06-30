
using MediatR;

namespace Booking.Application.ClientProfile.Queries.GetClientData;

public class UserDataQuery(int id) : IRequest<UserDataDto>
{
    public int Id { get; set; } = id;
}
