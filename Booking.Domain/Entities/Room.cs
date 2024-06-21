using Booking.Domain.Entities.Enums;


namespace Booking.Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public RoomType RoomType { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int NumberOfBeds { get; set; }
    public virtual Reservation? Reservation { get; set; }
    public int? ReservationId { get; set; }
    public virtual Hotel? Hotel { get; set; }
    public int HotelId { get; set; }
    public virtual List<Package>? Packages { get; set; }
}
