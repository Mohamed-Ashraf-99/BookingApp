
using MediatR;

namespace Booking.Application.AdminDashboard.Queries.GetAllUnVerifiedOwners;

public class GetUnVerifiedOwnersQuery : IRequest<IEnumerable<UnVerifiedOwnerDto>>
{
}
