using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string source { get; set; }

        public int IsMain { get; set; }

        public int HotelID { get; set; }
    }
}
