using AutoMapper;
using Booking.Application.UserComplains.Commands.Create;
using Booking.Domain.Entities;

namespace Booking.Application.UserComplains.Mapping;

public class ComplainsProfile : Profile
{
    public ComplainsProfile()
    {
        CreateMap<CreateComplainCommand, Complains>()
            .ForMember(x =>x.ClientId, opt => opt.MapFrom(src => src.ClientId))
            .ForMember(x =>x.AdminId, opt => opt.MapFrom(src => src.AdminId))
            .ForMember(x =>x.HotelId, opt => opt.MapFrom(src => src.HotelId))
            .ForMember(x =>x.Discription, opt => opt.MapFrom(src => src.Description))
            .ForMember(x =>x.Date, opt => opt.MapFrom(src => src.Date));
    }
}
