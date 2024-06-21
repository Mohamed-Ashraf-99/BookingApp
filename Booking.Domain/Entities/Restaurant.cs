
using Booking.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Domain.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? Rate { get; set; }
    public Ambience? Ambience { get; set; }
    public Cuisine? Cuisine { get; set; }
    public DietaryOptions? DietaryOptions { get; set; }
    public OpenFor? OpenFor { get; set; }
    public Hotel? Hotel { get; set; }
    public int? HotelId { get; set; }
    public Owner Owner { get; set; }
    public int? OwnerId { get; set; }
    public bool IsDeleted { get; set; }

}
