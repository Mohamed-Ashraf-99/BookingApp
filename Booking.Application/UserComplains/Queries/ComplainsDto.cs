

using Booking.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Application.UserComplains.Queries;

public class ComplainsDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string Discription { get; set; }
    public DateOnly Date { get; set; }
    public int HotelId { get; set; }
    [ForeignKey("OwnerId")]
    public int? OwnerId { get; set; }
    public int ClientId { get; set; }
    public bool? IsSolved { get; set; }

}
