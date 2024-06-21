using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfStars { get; set; }
        public Address? Address { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual List<Restaurant>? Restaurants { get; set; }
        public int OwnerId { get; set; }
        public virtual Owner? Owner { get; set; }
        public virtual List<WishList>? WishLists { get; set; }
        public virtual List<Reviews>? Reviews { get; set; }
        public virtual List<Offer> Offers { get; set; }
        public virtual List<Room> Rooms { get; set; }
        public virtual List<Complains>? Complains { get; set; }
    }
}
