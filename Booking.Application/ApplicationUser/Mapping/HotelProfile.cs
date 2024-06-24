using AutoMapper;
using Booking.Application.ApplicationUser.Commands.Register;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Mapping
{
    public class HotelProfile : Profile
    {
        public HotelProfile() {

            CreateMap<HotelDto, Hotel>()
               .ForMember(hotel => hotel.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(hotel => hotel.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(hotel => hotel.NumberOfStars, opt => opt.MapFrom(src => src.NumberOfStars))
               .ForPath(hotel => hotel.Address.City, opt => opt.MapFrom(src => src.Address.City))
               .ForPath(hotel => hotel.Address.Street, opt => opt.MapFrom(src => src.Address.Street))
               .ForPath(hotel => hotel.Address.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
               .ForMember(hotel => hotel.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
               .ForMember(hotel => hotel.Reviews, opt => opt.MapFrom(src => src.Reviews))
               .ForMember(hotel => hotel.Complains, opt => opt.MapFrom(src => src.Complains))
               .ForMember(hotel => hotel.Offers, opt => opt.MapFrom(src => src.Offers))
               .ForMember(hotel => hotel.Rooms, opt => opt.MapFrom(src => src.Rooms))
               .ForMember(hotel => hotel.WishLists, opt => opt.MapFrom(src => src.WishLists))
               .ForMember(hotel => hotel.Owner, opt => opt.MapFrom(src => src.Owner))
               .ForMember(hotel => hotel.Restaurants, opt => opt.MapFrom(src => src.Restaurants))
               .ForMember(hotel => hotel.Images, opt => opt.MapFrom(src => src.Images));

            // Mapping from Hotel to HotelDto
            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.NumberOfStars, opt => opt.MapFrom(src => src.NumberOfStars))
                .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.Address.City))
                .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForPath(dest => dest.Address.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.Complains, opt => opt.MapFrom(src => src.Complains))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms))
                .ForMember(dest => dest.Restaurants, opt => opt.MapFrom(src => src.Restaurants))
                .ForMember(dest => dest.WishLists, opt => opt.MapFrom(src => src.WishLists))
                .ForMember(dest => dest.Offers, opt => opt.MapFrom(src => src.Offers))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<Images, ImageDto>();
            CreateMap<ImageDto, Images>();

            CreateMap<Reviews, ReviewDto>();
            CreateMap<ReviewDto, Reviews>();

            CreateMap<Complains, ComplainDto>();
            CreateMap<ComplainDto, Complains>();

            CreateMap<Offer, OfferDto>();
            CreateMap<OfferDto, Offer>();

            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();

            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();

            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantDto, Restaurant>();

            CreateMap<WishList, WishListDto>();
            CreateMap<WishListDto, WishList>();

        }



    }
    
}
