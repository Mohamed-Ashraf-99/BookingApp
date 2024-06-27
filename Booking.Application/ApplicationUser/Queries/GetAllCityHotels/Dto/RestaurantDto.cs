using Booking.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Rate { get; set; }
        public string? Ambience { get; set; }
        public string? Cuisine { get; set; }
        public string? DietaryOptions { get; set; }
        public string? OpenFor { get; set; }
        public int? HotelId { get; set; }
      //  public String OwnerName { get; set; }
        public int? OwnerId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
