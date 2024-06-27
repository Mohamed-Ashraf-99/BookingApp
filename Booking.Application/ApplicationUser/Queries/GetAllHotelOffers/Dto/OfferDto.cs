using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;

namespace Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto
{
    public class OfferDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Discount { get; set; }
        public bool IsDeleted { get; set; }
        public int OwnerId { get; set; }
        public int HotelId { get; set; }

      //  public HotelOfferDto Hotel { get; set; }
    }

}
