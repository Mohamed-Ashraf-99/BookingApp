using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto
{
    public class WishListDto
    {
        public int Id { get; set; }
        public bool? IsDeleted { get; set; }
       // public Client Client { get; set; }
        public int? ClientId { get; set; }
    }
}
