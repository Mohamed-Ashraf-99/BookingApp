using AutoMapper;
using Booking.Application.ApplicationUser.Commands.Register;
using Booking.Application.ApplicationUser.Commands.UpdateUser;
using Booking.Application.ApplicationUser.Queries.GetAllUsers.Dto;
using Booking.Application.ApplicationUser.Queries.GetUserById.Dto;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Mapping
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<RegisterUserCommand, User>()
            .ForMember(user => user.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(user => user.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(user => user.PasswordHash, opt => opt.MapFrom(src => src.Password))
            .ForPath(user => user.Address.City, opt => opt.MapFrom(src => src.City))
            .ForPath(user => user.Address.Street, opt => opt.MapFrom(src => src.Street))
            .ForPath(user => user.Address.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(user => user.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<User, RegisterUserCommand>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForPath(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForPath(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForPath(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<UpdateUserCommand, User>()
               .ForMember(d => d.Address,
               opt => opt.MapFrom(
                   src => new Address
                   {
                       City = src.City,
                       Street = src.Street,
                       PostalCode = src.PostalCode,
                   }));

            CreateMap<User, GetUserListDto>()
           .ForMember(dto => dto.City, opt => opt.MapFrom(usr => usr.Address.City))
           .ForMember(dto => dto.Street, opt => opt.MapFrom(usr => usr.Address.Street))
           .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(usr => usr.Address.PostalCode));

            CreateMap<User, GetUserByIdDto>()
           .ForMember(dto => dto.City, opt => opt.MapFrom(usr => usr.Address.City))
           .ForMember(dto => dto.Street, opt => opt.MapFrom(usr => usr.Address.Street))
           .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(usr => usr.Address.PostalCode));


        }
    }
}
