
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.UserComplains.Queries.GetUsersComplains;

public class GetUserComplainsQueryHandler(IComplainsRepository _complainsRepository,
    ILogger<GetUserComplainsQueryHandler> _logger) : IRequestHandler<GetUserComplainsQuery, List<Complains>>
{

    public async Task<List<Complains>> Handle(GetUserComplainsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Get User Complains");
        return await _complainsRepository.GetComplainsByUserIdAsync(request.UserId);
    }
}