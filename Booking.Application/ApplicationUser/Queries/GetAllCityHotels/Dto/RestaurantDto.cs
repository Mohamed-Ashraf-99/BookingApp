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
        public Ambience? Ambience { get; set; }
        public Cuisine? Cuisine { get; set; }
        public DietaryOptions? DietaryOptions { get; set; }
        public OpenFor? OpenFor { get; set; }
        public int? HotelId { get; set; }
        public String OwnerName { get; set; }
        public int? OwnerId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
