namespace Booking.Domain.Entities.Enums;
[Flags]
public enum RoomType
{
    Single=0,
    Double=1,
    Suite=2,
    Deluxe=4,
    Family=8,
    Presidential=16
}
