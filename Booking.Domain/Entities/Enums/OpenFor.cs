

namespace Booking.Domain.Entities.Enums;

[Flags]
public enum OpenFor
{
    None = 0,
    Breakfast = 1,
    Lunch = 2,
    Dinner = 4,
    LateNight = 8
}
