namespace Booking.Domain.Entities.Enums;

[Flags]
public enum DietaryOptions
{
    None = 0,
    Vegetarian = 1,
    Vegan = 2,
    GlutenFree = 4,
    DairyFree = 8,
    NutFree = 16,
    Halal = 32,
    Kosher = 64
}
