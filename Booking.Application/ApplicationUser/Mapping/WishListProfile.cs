using AutoMapper;
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
            CreateMap<WishList, ClientWishListDto>().ForMember(dest => dest.Hotels, opt => opt.MapFrom(src => src.Hotels)); 

            CreateMap<Hotel, HotelWishListDto>();
        }
    }
}
