using Booking.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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
    public int? RoomId { get; set; }
    [ForeignKey("RoomId")]
    public virtual Room? Rooms { get; set; }
}
