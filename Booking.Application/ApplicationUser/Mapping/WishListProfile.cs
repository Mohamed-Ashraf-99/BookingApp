using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using Booking.Application.ApplicationUser.Queries.GetWishListByClientId.Dto;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Mapping
{
    public class WishListProfile : Profile
    {
        public WishListProfile() 
        {
            CreateMap<WishList, ClientWishListDto>()
                .ForMember(dest => dest.Hotels, opt => opt.MapFrom(src => src.HotelWishLists.Select(hw => hw.Hotel)))
                .ReverseMap()
                .ForMember(dest => dest.HotelWishLists, opt => opt.MapFrom(src => src.Hotels.Select(h => new HotelWishList { HotelsId = h.Id, WishListsId = src.Id })));

            CreateMap<Hotel, HotelWishListDto>()
                .ReverseMap()
                .ForMember(dest => dest.HotelWishLists, opt => opt.Ignore());

            CreateMap<Restaurant, RestaurantDto>().ReverseMap();
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<Reviews, ReviewDto>().ReverseMap();
            CreateMap<Offer, OfferDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Complains, ComplainDto>().ReverseMap();
            CreateMap<Images, ImageDto>().ReverseMap();
        }
    }
    }

