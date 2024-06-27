using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using Booking.Application.ApplicationUser.Queries.GetHotelById.Dto;
using Booking.Domain.Entities;
using System.Collections.Generic;

namespace Booking.Application.ApplicationUser.Mapping
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.NumberOfStars, opt => opt.MapFrom(src => src.NumberOfStars))
                .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.Address.City))
                .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForPath(dest => dest.Address.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.Complains, opt => opt.MapFrom(src => src.Complains))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms))
                .ForMember(dest => dest.Restaurants, opt => opt.MapFrom(src => src.Restaurants))
                //.ForMember(dest => dest.WishLists, opt => opt.MapFrom(src => src.WishLists))
                .ForMember(dest => dest.Offers, opt => opt.MapFrom(src => src.Offers))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<HotelDto, Hotel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.NumberOfStars, opt => opt.MapFrom(src => src.NumberOfStars))
                .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.Address.City))
                .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForPath(dest => dest.Address.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.Complains, opt => opt.MapFrom(src => src.Complains))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms))
                .ForMember(dest => dest.Restaurants, opt => opt.MapFrom(src => src.Restaurants))
                //.ForMember(dest => dest.WishLists, opt => opt.MapFrom(src => src.WishLists))
                .ForMember(dest => dest.Offers, opt => opt.MapFrom(src => src.Offers))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<Images, ImageDto>();
            CreateMap<ImageDto, Images>();

            CreateMap<Reviews, ReviewDto>();
            CreateMap<ReviewDto, Reviews>();

            CreateMap<Complains, ComplainDto>();
            CreateMap<ComplainDto, Complains>();

            CreateMap<Offer, OfferDto>();
            CreateMap<OfferDto, Offer>();

            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.NumberOfBeds, opt => opt.MapFrom(src => src.NumberOfBeds))
                .ForMember(dest => dest.Reservation, opt => opt.MapFrom(src => src.Reservation))
                .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.ReservationId))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.Packages, opt => opt.MapFrom(src => src.Packages));

            CreateMap<RoomDto, Room>()
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.NumberOfBeds, opt => opt.MapFrom(src => src.NumberOfBeds))
                .ForMember(dest => dest.Reservation, opt => opt.MapFrom(src => src.Reservation))
                .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.ReservationId))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.Packages, opt => opt.MapFrom(src => src.Packages));

            CreateMap<Package, PackageDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.RoomFacilities, opt => opt.MapFrom(src => src.RoomFacilities))
                .ForMember(dest => dest.Meals, opt => opt.MapFrom(src => src.Meals))
                .ForMember(dest => dest.PackageFacilities, opt => opt.MapFrom(src => src.PackageFacilities));

            CreateMap<PackageDto, Package>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.RoomFacilities, opt => opt.MapFrom(src => src.RoomFacilities))
                .ForMember(dest => dest.Meals, opt => opt.MapFrom(src => src.Meals))
                .ForMember(dest => dest.PackageFacilities, opt => opt.MapFrom(src => src.PackageFacilities));

            CreateMap<RoomFacilities, RoomFacilitiesDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => src.ViewType));

            CreateMap<RoomFacilitiesDto, RoomFacilities>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => src.ViewType));

            CreateMap<Meals, MealsDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<MealsDto, Meals>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<PackageFacilities, PackageFacilitiesDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<PackageFacilitiesDto, PackageFacilities>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
                .ForMember(dest => dest.Ambience, opt => opt.MapFrom(src => src.Ambience))
                .ForMember(dest => dest.Cuisine, opt => opt.MapFrom(src => src.Cuisine))
                .ForMember(dest => dest.DietaryOptions, opt => opt.MapFrom(src => src.DietaryOptions))
                .ForMember(dest => dest.OpenFor, opt => opt.MapFrom(src => src.OpenFor))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
               // .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<RestaurantDto, Restaurant>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
                .ForMember(dest => dest.Ambience, opt => opt.MapFrom(src => src.Ambience))
                .ForMember(dest => dest.Cuisine, opt => opt.MapFrom(src => src.Cuisine))
                .ForMember(dest => dest.DietaryOptions, opt => opt.MapFrom(src => src.DietaryOptions))
                .ForMember(dest => dest.OpenFor, opt => opt.MapFrom(src => src.OpenFor))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
               // .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));
        }
    }
}
