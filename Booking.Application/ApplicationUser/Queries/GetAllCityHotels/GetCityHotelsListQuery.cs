using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllCityHotels
{
    public class GetCityHotelsListQuery(string city): IRequest<IEnumerable<HotelDto>> 
    {
        public string City { get; set; } = city;
    }
}
