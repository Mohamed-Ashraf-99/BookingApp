using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Mapping
{
    public class OfferProfile : Profile
    {

        public OfferProfile() {


            CreateMap<OfferDto, Offer>()
                  .ForMember(offer => offer.StartDate, opt => opt.MapFrom(src => src.StartDate))
                  .ForMember(offer => offer.Description, opt => opt.MapFrom(src => src.Description))
                  .ForMember(offer => offer.EndDate, opt => opt.MapFrom(src => src.EndDate))
                  .ForMember(offer => offer.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                  .ForMember(offer => offer.Discount, opt => opt.MapFrom(src => src.Discount))
                  .ForMember(offer => offer.OwnerId, opt => opt.MapFrom(src => src.OwnerId));

            // Mapping from Hotel to HotelDto
            CreateMap<Offer, OfferDto>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));


            CreateMap<Hotel, HotelOfferDto>()
            .ForMember(img => img.Images, opt => opt.MapFrom(src => src.Images));
            CreateMap<HotelOfferDto,Hotel>()
             .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
          

            CreateMap<Images, ImageDto>();
            CreateMap<ImageDto, Images>();

        }
    }
}
