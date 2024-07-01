
using Booking.Domain.Entities;
using MediatR;

namespace Booking.Application.UserComplains.Queries.GetUsersComplains;

public class GetUserComplainsQuery : IRequest<List<ComplainsDto>>
{
    public int UserId { get; set; }
}
