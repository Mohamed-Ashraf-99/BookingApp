using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllHotelOffers
{
    public class GetAllHotelOffersListQuery:IRequest<IEnumerable<OfferDto>>
    {

    }
}
