using AutoMapper;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Booking.Queries.ClientReservations;

public class ClientReservationsQueryHandler(ILogger<ClientReservationsQueryHandler> _logger,
    IReservationRepository _reservationRepository,
    IMapper _mapper) : IRequestHandler<ClientReservationsQuery, IEnumerable<ReservationDto>>
{
    public async Task<IEnumerable<ReservationDto>> Handle(ClientReservationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Get Reservations to user with id : {request.userId}");
        var reservations = await _reservationRepository.GetClientReservationByUserIdAsync(request.userId);
        var result = _mapper.Map<IEnumerable<ReservationDto>>( reservations );
        return result;
    }
}
