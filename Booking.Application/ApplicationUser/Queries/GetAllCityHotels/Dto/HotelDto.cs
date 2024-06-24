using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfStars { get; set; }
        public Address Address { get; set; }
        public bool? IsDeleted { get; set; }
        public List<RestaurantDto> Restaurants { get; set; } = new List<RestaurantDto>();
        public int OwnerId { get; set; }
        public OwnerDto Owner { get; set; }
        public List<WishListDto> WishLists { get; set; } = new List<WishListDto>();
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();

        public List<OfferDto> Offers { get; set; } = new List<OfferDto>();
        public List<RoomDto> Rooms { get; set; } = new List<RoomDto>();
        public List<ComplainDto> Complains { get; set; } = new List<ComplainDto>();
        public List<ImageDto> Images { get; set; } = new List<ImageDto>();
    }
}
