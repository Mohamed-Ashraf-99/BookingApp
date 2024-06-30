using AutoMapper;
using Booking.Application.Booking.Commands.CreateReservation;
using Booking.Application.Booking.Queries.ClientReservations;
using Booking.Domain.Entities;

namespace Booking.Application.Booking.Mapping;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<CreateReservationCommand, Reservation>();
        CreateMap<ReservationDto, Reservation>()
            .ForMember(dest => dest.Rooms, opt => opt.Ignore())
            .ReverseMap();
    }
}
