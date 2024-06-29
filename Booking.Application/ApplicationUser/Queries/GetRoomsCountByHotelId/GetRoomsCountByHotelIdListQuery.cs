using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetRoomsCountByHotelId
{
    public class GetRoomsCountByHotelIdListQuery(int hotelId) : IRequest<int>
    {
        public int HotelId { get; set; } = hotelId;
    }
}
