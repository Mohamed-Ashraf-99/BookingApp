namespace Booking.Domain.Entities.Enums;
[Flags]
public enum ViewType
{
    SeaView=0,
    MountainView=1,
    CityView=2,
    GardenView=4,
    PoolView=8,
    NoView=16
}
