using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllReviews.Dto;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelDto = Booking.Application.ApplicationUser.Queries.GetAllReviews.Dto.HotelDto;

namespace Booking.Application.ApplicationUser.Mapping
{
    public class ReviewsProfile : Profile
    {
        public ReviewsProfile() {
            CreateMap<Reviews, RevieewDto>()
           .ForMember(dest => dest.Hotel, opt => opt.MapFrom(src => src.Hotel))
           .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client));
            CreateMap<Hotel, HotelDto>();
            // CreateMap<Client, ClientDto>();
            CreateMap<Client, ClientDto>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
