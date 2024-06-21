using AutoMapper;
using Booking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllUsers.Dto
{
    public class GetUserListProfile : Profile
    {
        public GetUserListProfile()
        {
            CreateMap<User, GetUserListDto>()
           .ForMember(dto => dto.City, opt => opt.MapFrom(usr => usr.Address.City))
           .ForMember(dto => dto.Street, opt => opt.MapFrom(usr => usr.Address.Street))
           .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(usr => usr.Address.PostalCode));

        }
    }
}
