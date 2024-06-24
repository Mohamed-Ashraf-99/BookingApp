using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public decimal? Rate { get; set; }
        public string Comment { get; set; }
        public int HotelId { get; set; }
        public int? ClientId { get; set; }
    }
}
