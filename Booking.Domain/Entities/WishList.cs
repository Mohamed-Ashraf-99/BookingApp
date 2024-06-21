

namespace Booking.Domain.Entities;

public class WishList
{
    public int Id { get; set; }
    public bool? IsDeleted { get; set; }
    public List<Hotel>? Hotels { get; set; }
    public Client? Client { get; set; }
    public int? ClientId { get; set; }
}
