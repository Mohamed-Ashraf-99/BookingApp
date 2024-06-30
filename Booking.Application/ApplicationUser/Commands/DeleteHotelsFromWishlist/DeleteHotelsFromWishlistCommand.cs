using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.DeleteHotelsFromWishlist
{
    public class DeleteHotelsFromWishlistCommand : IRequest<string>
    {
        public int userId { get; set; }
        public int hotelId { get; set; }

    }
}
