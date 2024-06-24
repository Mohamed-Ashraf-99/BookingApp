using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllHotels
{
    public class GetAllHotelsListQuery:IRequest<IEnumerable<HotelDto>>
    {

    }
}
