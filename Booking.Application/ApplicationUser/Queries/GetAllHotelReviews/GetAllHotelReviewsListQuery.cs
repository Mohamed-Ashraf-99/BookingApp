using Booking.Application.ApplicationUser.Queries.GetAllReviews.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllHotelReviews
{
    public class GetAllHotelReviewsListQuery (int hotelID): IRequest<IEnumerable<RevieewDto>>
    {
        public int HotelID { get; set; } = hotelID;
    }
}
