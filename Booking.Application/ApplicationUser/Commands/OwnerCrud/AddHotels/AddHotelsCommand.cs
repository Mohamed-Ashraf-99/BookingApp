using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.OwnerCrud.AddHotels
{
    public class AddHotelsCommand : IRequest<string>
    {
        public string name { get; set; }

        public string description { get; set; }

        public int numOfStars { get; set; }

        public string city { get; set; }

        public string street { get; set; }

        public string postalCode { get; set; }

        public int userId { get; set; }

        public List<string>? imagesSource { get; set; } = new List<string>();

        //public List<RoomDto>? rooms { get; set; } = new List<RoomDto>();
        
    }
}
