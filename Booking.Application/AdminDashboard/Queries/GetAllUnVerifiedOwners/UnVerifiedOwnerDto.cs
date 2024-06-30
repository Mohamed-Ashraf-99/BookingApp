
namespace Booking.Application.AdminDashboard.Queries.GetAllUnVerifiedOwners;

public class UnVerifiedOwnerDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Certificate { get; set; }
    public string PhoneNumber { get; set; }
}
