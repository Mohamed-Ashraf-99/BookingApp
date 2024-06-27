using Booking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public virtual User? User { get; set; }
        public virtual List<Hotel>? Hotels { get; set; }
        public virtual List<Offer>?  Offers { get; set; }
        public virtual List<Restaurant>?  Restaurants { get; set; }
        public virtual List<Complains>? Complains { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }
}
