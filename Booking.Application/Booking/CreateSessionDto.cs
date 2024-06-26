using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Booking
{
    public class CreateSessionDto
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
        public decimal Amount { get; set; }
    }
}
