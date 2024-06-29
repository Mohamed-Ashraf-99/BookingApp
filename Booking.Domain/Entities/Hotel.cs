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
        public virtual List<Restaurant>? Restaurants { get; set; }= new List<Restaurant>();
        public int OwnerId { get; set; }
        public virtual Owner? Owner { get; set; }
        //public virtual List<WishList>? WishLists { get; set; }
        public virtual List<Reviews>? Reviews { get; set; }=new List<Reviews>();
        public virtual List<Offer> Offers { get; set; } = new List<Offer>();
        public virtual List<Room> Rooms { get; set; } = new List<Room>();
        public virtual List<Complains>? Complains { get; set; } = new List<Complains>();
        public virtual List<Images>? Images { get; set; } = new List<Images>();
        public virtual List<HotelWishList> HotelWishLists { get; set; } = new List<HotelWishList>();


    }
}
