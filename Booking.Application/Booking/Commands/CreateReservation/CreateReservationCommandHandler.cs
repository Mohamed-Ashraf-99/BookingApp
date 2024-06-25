using AutoMapper;
using Booking.Application.Services.CurrentUser;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Booking.Commands.CreateReservation;

public class CreateReservationCommandHandler(ILogger<CreateReservationCommandHandler> _logger,
    IMapper _mapper,
    IReservationRepository _reservationRepository,
    IRoomRepository _roomRepository,
    ICurrentUserService _currentUserService,
    IClientRepository _clientRepository) : IRequestHandler<CreateReservationCommand, string>
{
    public async Task<string> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create a reservation");
        var reservation = _mapper.Map<Reservation>(request);
        foreach (var roomId in request.RoomIds)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);
            if (room != null)
            {
                reservation.Rooms.Add(room);
            }
        }
        var userId = await _currentUserService.GetUserId();
        var clientId = await _clientRepository.GetClientIdByUserId(userId);
        reservation.ClientId = clientId;
        var response = await _reservationRepository.CreateAsync(reservation);
        if (response > 0)
            return "Succeeded";
        return "Failed";
    }
}
