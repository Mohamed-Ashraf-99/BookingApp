using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class Images
    {
        public int Id { get; set; }
        public string source { get; set; }

        public int IsMain { get; set; }

        public int HotelID { get; set; }
        public Hotel Hotel { get; set; }
    }
}
