
using AutoMapper;
using Booking.Application.ClientProfile.Commands.UpdateProfile;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;

namespace Booking.Application.ClientProfile.Mapping;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<UpdateProfileCommand, User>()
            .ForMember(dest => dest.Image, opt => opt.Ignore()) // Handle image separately
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode
            }))
            .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));

    }
}
