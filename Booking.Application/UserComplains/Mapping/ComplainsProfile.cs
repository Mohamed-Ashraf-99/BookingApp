using AutoMapper;
using Booking.Application.UserComplains.Commands.Create;
using Booking.Application.UserComplains.Queries;
using Booking.Domain.Entities;

namespace Booking.Application.UserComplains.Mapping;

public class ComplainsProfile : Profile
{
    public ComplainsProfile()
    {
        CreateMap<CreateComplainCommand, Complains>()
            .ForMember(x =>x.ClientId, opt => opt.MapFrom(src => src.ClientId))
            .ForMember(x =>x.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
            .ForMember(x =>x.HotelId, opt => opt.MapFrom(src => src.HotelId))
            .ForMember(x =>x.Discription, opt => opt.MapFrom(src => src.Description))
            .ForMember(x =>x.Date, opt => opt.MapFrom(src => src.Date));

        CreateMap<Complains, ComplainsDto>()
        .ForMember(dest => dest.IsSolved, opt => opt.MapFrom(src => src.IsSolved))
        .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Date)))
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ClientId))
        .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
        .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
        .ForMember(dest => dest.Discription, opt => opt.MapFrom(src => src.Discription))
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Client.User.UserName))
        .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.Client.User.Email));
    }
}
