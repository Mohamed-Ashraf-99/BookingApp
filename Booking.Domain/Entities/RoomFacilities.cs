
using Booking.Domain.Entities.Enums;

namespace Booking.Domain.Entities;

public class RoomFacilities
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ViewType ViewType { get; set; }
    public virtual List<Package>? Packages { get; set; }
}
