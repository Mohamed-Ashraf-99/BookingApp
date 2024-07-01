using AutoMapper;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.UserComplains.Queries.GetHotelComplains;

public class GetHotelComplainsQueryHandler(IComplainsRepository _complainsRepository,
    ILogger<GetHotelComplainsQueryHandler> _logger,
    IMapper _mapper) : IRequestHandler<GetHotelComplainsQuery, List<ComplainsDto>>
{
    public async Task<List<ComplainsDto>> Handle(GetHotelComplainsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Get Complains for Hotel with id : {request.HotelId}");
        var complains = await _complainsRepository.GetComplainsByHotelIdAsync(request.HotelId);
        var complainsDto = _mapper.Map<List<ComplainsDto>>(complains);
        return complainsDto;
    }
}