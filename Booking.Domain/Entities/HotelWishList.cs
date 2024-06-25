using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class HotelWishList
    {
        public int HotelsId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public int WishListsId { get; set; }
        public virtual WishList WishList { get; set; }
    }
}
