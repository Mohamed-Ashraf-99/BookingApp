
using AutoMapper;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.UserComplains.Queries.GetUsersComplains;

public class GetUserComplainsQueryHandler(IComplainsRepository _complainsRepository,
    ILogger<GetUserComplainsQueryHandler> _logger,
    IMapper _mapper) : IRequestHandler<GetUserComplainsQuery, List<ComplainsDto>>
{

    public async Task<List<ComplainsDto>> Handle(GetUserComplainsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Get User Complains");
        var complains = await _complainsRepository.GetComplainsByUserIdAsync(request.UserId);
        var complainsDto = _mapper.Map<List<ComplainsDto>>(complains);
        return complainsDto;
    }
}