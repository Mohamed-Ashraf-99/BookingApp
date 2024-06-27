using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto
{
    public class ComplainDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool? IsSolved { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime Date { get; set; }
        public int HotelId { get; set; }
        public Admin Admin { get; set; }
        public int AdminId { get; set; }
        //public Client Client { get; set; }
        public int ClientId { get; set; }

        public string ClientName { get; set; }
    }
}
