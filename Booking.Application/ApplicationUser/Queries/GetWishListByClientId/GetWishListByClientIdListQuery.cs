using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetWishListByClientId.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetWishListByClientId
{
    public class GetWishListByClientIdListQuery(int userId) : IRequest<ClientWishListDto>
    {
        public int userId { get; set; } = userId;
    }
}
