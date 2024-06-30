
using Booking.Domain.Entities;
using MediatR;

namespace Booking.Application.Booking.Queries.ClientReservations;

public class ClientReservationsQuery(int id) : IRequest<IEnumerable<ReservationDto>>
{
    public int userId { get; set; } = id;
}
