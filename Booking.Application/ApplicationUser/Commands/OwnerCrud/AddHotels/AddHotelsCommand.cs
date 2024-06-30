using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.OwnerCrud.AddHotels
{
    public class AddHotelsCommand : IRequest<string>
    {
        [Required]
        public string name { get; set; }
        [Required]

        public string description { get; set; }
        [Required]

        public int numOfStars { get; set; }
        [Required]

        public string city { get; set; }
        [Required]

        public string street { get; set; }
        [Required]

        public string postalCode { get; set; }
        [Required]

        public int userId { get; set; }

        public List<IFormFile>? imagesSource { get; set; } = new List<IFormFile>();

        //public List<RoomDto>? rooms { get; set; } = new List<RoomDto>();
        
    }
}
