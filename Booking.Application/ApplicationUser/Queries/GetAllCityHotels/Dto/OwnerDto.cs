using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using Booking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto
{
    public class OwnerDto
    {
        public int Id { get; set; }
        public List<OfferDto> Offers { get; set; }
        public List<RestaurantDto> Restaurants { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
