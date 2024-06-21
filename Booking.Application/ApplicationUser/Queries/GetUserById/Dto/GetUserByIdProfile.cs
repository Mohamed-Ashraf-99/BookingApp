using AutoMapper;
using Booking.Domain.Entities.Identity;

namespace Booking.Application.ApplicationUser.Queries.GetUserById.Dto;

public class GetUserByIdProfile : Profile
{
    public GetUserByIdProfile()
    {
        CreateMap<User, GetUserByIdDto>()
       .ForMember(dto => dto.City, opt => opt.MapFrom(usr => usr.Address.City))
       .ForMember(dto => dto.Street, opt => opt.MapFrom(usr => usr.Address.Street))
       .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(usr => usr.Address.PostalCode));
    }
}
