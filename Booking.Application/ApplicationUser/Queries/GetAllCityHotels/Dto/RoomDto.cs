using Booking.Domain.Entities;
using Booking.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto
{
    public class RoomDto
    {
        public int Id { get; set; }
        public RoomType RoomType { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int NumberOfBeds { get; set; }
        public Reservation Reservation { get; set; }
        public int? ReservationId { get; set; }
        public int HotelId { get; set; }
        public List<Package> Packages { get; set; }
    }
}
