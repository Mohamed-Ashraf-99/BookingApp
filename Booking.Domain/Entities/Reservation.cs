using Booking.Domain.Entities.Enums;

namespace Booking.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfGuests { get; set; }
    public bool IsDeleted { get; set; }
    public virtual Client? Client { get; set; }
    public int ClientId { get; set; }
    public string? PaymentIntentId  { get; set; }
    public decimal? Amount { get; set; }
    public ReservationState? State {  get; set; }
    public virtual List<Room>? Rooms { get; set; } = new List<Room>();
}
