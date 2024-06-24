using AutoMapper;
using Booking.Application.Authorization.Commands.CreateRole;
using Booking.Application.Authorization.Queries.GetAllRoles.Dto;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Authorization.Mapping;

public class AuthorizationProfile : Profile
{
    public AuthorizationProfile()
    {
        CreateMap<CreateRoleCommand, IdentityRole>()
            .ForMember(x => x.Name, y => y.MapFrom(src => src.RoleName));

        CreateMap<IdentityRole<int>, GetRolesDto>()
              .ForMember(x => x.RoleName, y => y.MapFrom(src => src.Name))
              .ForMember(x => x.RoleId, y => y.MapFrom(src => src.Id));
    }
}
