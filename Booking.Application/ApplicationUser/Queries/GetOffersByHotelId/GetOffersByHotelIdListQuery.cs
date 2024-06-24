using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetOffersByHotelId
{
    public class GetOffersByHotelIdListQuery(int hotelId) : IRequest<IEnumerable<OfferDto>>
    {
        public int HotelId { get; set; } = hotelId;

    }
}
