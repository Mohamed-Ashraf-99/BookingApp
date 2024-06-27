using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Mapping
{
    public class RestaurantBaseProfile: Profile
    {
        public RestaurantBaseProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
           .ForMember(dest => dest.Ambience, opt => opt.MapFrom(src => src.Ambience.HasValue ? src.Ambience.Value.ToString() : null))
           .ForMember(dest => dest.Cuisine, opt => opt.MapFrom(src => src.Cuisine.HasValue ? src.Cuisine.Value.ToString() : null))
           .ForMember(dest => dest.DietaryOptions, opt => opt.MapFrom(src => src.DietaryOptions.HasValue ? src.DietaryOptions.Value.ToString() : null))
           .ForMember(dest => dest.OpenFor, opt => opt.MapFrom(src => src.OpenFor.HasValue ? src.OpenFor.Value.ToString() : null))
           .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
           .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<RestaurantDto, Restaurant>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
            .ForMember(dest => dest.Ambience, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Ambience) ? (Ambience?)null : Enum.Parse<Ambience>(src.Ambience)))
            .ForMember(dest => dest.Cuisine, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Cuisine) ? (Cuisine?)null : Enum.Parse<Cuisine>(src.Cuisine)))
            .ForMember(dest => dest.DietaryOptions, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.DietaryOptions) ? (DietaryOptions?)null : Enum.Parse<DietaryOptions>(src.DietaryOptions)))
            .ForMember(dest => dest.OpenFor, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.OpenFor) ? (OpenFor?)null : Enum.Parse<OpenFor>(src.OpenFor)))
          .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
          .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));
          


        }
    }
}
