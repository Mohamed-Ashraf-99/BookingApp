using AutoMapper;
using Booking.Application.Authorization.Commands.CreateRole;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Authorization.Mapping;

public class AuthorizationProfile : Profile
{
    public AuthorizationProfile()
    {
        CreateMap<CreateRoleCommand, IdentityRole>()
            .ForMember(x => x.Name, y => y.MapFrom(src => src.RoleName));
    }
}
