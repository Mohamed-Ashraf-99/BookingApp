using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.AddHotelsToWishList
{
    public class AddHotelsToWishListCommand : IRequest<string>
    {
        public int clientId { get; set; }
        public int HotelId { get; set; }    
    }
}
