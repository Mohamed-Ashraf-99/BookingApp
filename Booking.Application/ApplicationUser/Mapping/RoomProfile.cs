using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetHotelById.Dto;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Mapping
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();

            //CreateMap<Reservation, ReservationDto>();
            //CreateMap<ReservationDto, Reservation>();

            CreateMap<Package, PackageDto>()
                .ForMember(dest => dest.RoomFacilities, opt => opt.MapFrom(src => src.RoomFacilities))
                .ForMember(dest => dest.Meals, opt => opt.MapFrom(src => src.Meals))
                .ForMember(dest => dest.PackageFacilities, opt => opt.MapFrom(src => src.PackageFacilities));
            CreateMap<PackageDto, Package>();

            CreateMap<RoomFacilities, RoomFacilitiesDto>();
            CreateMap<RoomFacilitiesDto, RoomFacilities>();

            CreateMap<Meals, MealsDto>();
            CreateMap<MealsDto, Meals>();

            CreateMap<PackageFacilities, PackageFacilitiesDto>();
            CreateMap<PackageFacilitiesDto, PackageFacilities>();
        }
    }
}
