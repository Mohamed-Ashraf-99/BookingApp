using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class Complains
    {
        public int Id { get; set; }
        public string Discription { get; set; }
        public bool? IsSolved { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime Date { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel? Hotel { get; set; }
        public virtual Admin? Admin { get; set; }
        public int AdminId { get; set; }
        public virtual Client? Client { get; set; }
        public int ClientId { get; set; }

    }
}
