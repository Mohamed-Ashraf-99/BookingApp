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
    ILogger<GetHotelComplainsQueryHandler> _logger) : IRequestHandler<GetHotelComplainsQuery, List<Complains>>
{
    public async Task<List<Complains>> Handle(GetHotelComplainsQuery request, CancellationToken cancellationToken)
    {
        return await _complainsRepository.GetComplainsByHotelIdAsync(request.HotelId);
    }
}