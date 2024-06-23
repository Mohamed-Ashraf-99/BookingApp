namespace Booking.Domain.Entities;

public class Offer
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Discount { get; set; }
    public bool IsDeleted { get; set; }
    public int OwnerId { get; set; }
    public Owner? Owner { get; set; }
    public Hotel? Hotel { get; set; }
    public int HotelId { get; set; }
}
