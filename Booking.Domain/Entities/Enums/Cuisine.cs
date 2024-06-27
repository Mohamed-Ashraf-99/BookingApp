namespace Booking.Domain.Entities.Enums;

[Flags]
public enum Cuisine
{
    Italian=0,
    Chinese=1,
    Indian=2,
    French=4,
    Japanese=8,
    Mexican=16,
    American=32,
    Mediterranean=64
}
