using AutoMapper;
using Booking.Application.AdminDashboard.Queries.GetAllUnVerifiedOwners;
using Booking.Domain.Entities;

namespace Booking.Application.AdminDashboard.Mapping;

public class ApproveOwnersProfile : Profile
{
    public ApproveOwnersProfile()
    {
        CreateMap<Owner, UnVerifiedOwnerDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User != null ? src.User.Id : 0))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Certificate, opt => opt.MapFrom(src => src.Certificate))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));
    }
}
