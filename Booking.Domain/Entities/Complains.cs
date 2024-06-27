using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class Complains
    {
        public int Id { get; set; }
        public string Discription { get; set; }
        public bool? IsSolved { get; set; } = false;
        public bool? IsDeleted { get; set; } = false;
        public DateTime Date { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel? Hotel { get; set; }
        [ForeignKey("OwnerId")]
        public virtual Owner? Owner { get; set; }
        public int? OwnerId { get; set; }
        public virtual Client? Client { get; set; }
        public int ClientId { get; set; }

    }
}
