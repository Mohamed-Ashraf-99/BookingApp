
using Booking.Domain.Entities.Enums;
using Booking.Domain.Entities;

namespace Booking.Application.Booking.Queries.ClientReservations;

public class ReservationDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfGuests { get; set; }
    public int ClientId { get; set; }
    public decimal? Amount { get; set; }
    public ReservationState? State { get; set; }
    public int? RoomId { get; set; }
}
