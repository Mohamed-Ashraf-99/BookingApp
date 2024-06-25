using AutoMapper;
using Booking.Application.Booking.Commands.CreateReservation;
using Booking.Domain.Entities;

namespace Booking.Application.Booking.Mapping;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<CreateReservationCommand, Reservation>();
    }
}
