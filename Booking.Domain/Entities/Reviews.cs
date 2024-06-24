using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class Reviews
    {
        public int Id { get; set; }
        public decimal? Rate { get; set; }
        public string? Comment { get; set; }
        public Hotel? Hotel { get; set; }
        public int HotelId { get; set; }
        public Client? Client { get; set; }
        public int? ClientId { get; set; }
        public DateTime? Date { get; set; }
    }
}
