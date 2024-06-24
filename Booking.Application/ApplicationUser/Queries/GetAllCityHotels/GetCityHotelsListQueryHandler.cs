using Booking.Application.ApplicationUser.Queries.GetAllUsers.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllUsers;
using Booking.Application.ApplicationUser.Queries.GetUserById;
using Booking.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Exceptions;
using Booking.Domain.Repositories;
using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;

namespace Booking.Application.ApplicationUser.Queries.GetAllCityHotels
{
    public class GetCityHotelsListQueryHandler(ILogger<GetCityHotelsListQueryHandler> _logger 
        , IMapper mapper
        , IHotelRepository _hotelRepository) : IRequestHandler<GetCityHotelsListQuery, IEnumerable<HotelDto>>
    {
        public async Task<IEnumerable<HotelDto>> Handle(GetCityHotelsListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Retrieving hotels in city: {request.City}");

                var hotels = await _hotelRepository.GetHotelsByCityAsync(request.City);

                if (hotels == null || !hotels.Any())
                {
                    _logger.LogWarning("No hotels found in the specified city");
                    throw new NotFoundException($"No hotels found in {request.City}");
                }

                var hotelDtos = mapper.Map<IEnumerable<HotelDto>>(hotels);

                _logger.LogInformation($"Successfully retrieved {hotelDtos.Count()} hotels in {request.City}");

                return hotelDtos;

            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, $"Hotels not found in {request.City}");
                throw new NotFoundException($"No hotels found in {request.City}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving hotels");
                throw new ApplicationException("An unexpected error occurred while retrieving hotels", ex);
            }

        }


    }
}
