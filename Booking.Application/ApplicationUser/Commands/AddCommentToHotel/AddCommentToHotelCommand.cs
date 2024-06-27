using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.AddCommentToHotel
{
    public class AddCommentToHotelCommand (int clientid, int hotelid) : IRequest<string>
    {
        public string? CommentText { get; set; }
        public decimal? Rate { get; set; }  
        public int clientId { get; set; } = clientid; 
        public int hotelId { get; set; } = hotelid;

    }
}
