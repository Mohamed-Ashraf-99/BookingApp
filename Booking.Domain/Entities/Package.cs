
namespace Booking.Domain.Entities;

public class Package
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public virtual List<RoomFacilities>? RoomFacilities { get; set; }
    public virtual List<Meals>? Meals { get; set; }
    public virtual List<PackageFacilities>? PackageFacilities { get; set; }
}
