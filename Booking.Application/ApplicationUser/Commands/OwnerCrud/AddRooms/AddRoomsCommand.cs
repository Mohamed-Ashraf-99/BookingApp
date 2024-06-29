using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.OwnerCrud.AddRooms
{
    public class AddRoomsCommand :IRequest<string>
    {
        public string roomType { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int numberOfBeds { get; set; }
        public int hotelId { get; set; }
    }
}
