namespace Booking.Domain.Entities.Enums;

[Flags]
public enum Ambience
{
    Casual=0,
    Formal=1,
    Romantic=2,
    FamilyFriendly=4,
    Trendy=8,
    Cozy=16
}
