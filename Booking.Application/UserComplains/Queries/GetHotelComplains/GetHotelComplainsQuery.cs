using Booking.Domain.Entities;
using MediatR;

namespace Booking.Application.UserComplains.Queries.GetHotelComplains;

public class GetHotelComplainsQuery : IRequest<List<Complains>>
{
    public int HotelId { get; set; }
}