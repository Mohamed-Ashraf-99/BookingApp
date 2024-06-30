
using AutoMapper;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.AdminDashboard.Queries.GetAllUnVerifiedOwners;

public class GetUnVerifiedOwnersQueryHandler(IOwnerRepository _ownerRepository,
    ILogger<GetUnVerifiedOwnersQueryHandler> _logger,
    IMapper _mapper) : IRequestHandler<GetUnVerifiedOwnersQuery, IEnumerable<UnVerifiedOwnerDto>>
{
    public async Task<IEnumerable<UnVerifiedOwnerDto>> Handle(GetUnVerifiedOwnersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get All UnVerified Owners");
        var unVerifiedOwners = await _ownerRepository.GetAllUnVerifiedOwners();
        var unVerifiedOwnersDto = _mapper.Map<IEnumerable<UnVerifiedOwnerDto>>(unVerifiedOwners);
        return unVerifiedOwnersDto;
    }
}
