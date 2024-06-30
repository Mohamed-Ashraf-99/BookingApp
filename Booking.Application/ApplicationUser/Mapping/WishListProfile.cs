using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllReviews.Dto;
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
                 .ReverseMap();

            CreateMap<Hotel, HotelWishListDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ReverseMap();

           

            CreateMap<Reviews, RevieewDto>()
              .ForMember(dest => dest.Hotel, opt => opt.MapFrom(src => src.Hotel))
              .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
             .ReverseMap();


            CreateMap<Images, ImageDto>();
            CreateMap<ImageDto, Images>();

            // Mapping for Owner to OwnerDto
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto,Owner> ();

            CreateMap<Client, ClientDto>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.UserName));


        }
    }
    }

