using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.AddCommentToHotel
{
    public class AddCommentToHotelCommand (int Userid, int hotelid) : IRequest<string>
    {
        public string? CommentText { get; set; }
        public decimal? Rate { get; set; }  
        public int userId { get; set; } = Userid; 
        public int hotelId { get; set; } = hotelid;

    }
}
