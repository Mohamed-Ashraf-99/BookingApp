using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllReviews.Dto
{
    public class RevieewDto
    {
        public int Id { get; set; }
        public HotelDto Hotel { get; set; }
        public ClientDto Client { get; set; }
        public string Comment { get; set; }
        public decimal Rate { get; set; }
    }
}
