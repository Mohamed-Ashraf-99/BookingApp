using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.OwnerCrud.AddOffers
{
    public class AddOffersCommand : IRequest<string>
    {
        public string description {  get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public decimal discount { get; set; }

        public int userId { get; set; }

        public int hotelId { get; set; }
    }

}
