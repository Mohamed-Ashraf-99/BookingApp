using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;

namespace Booking.Application.ApplicationUser.Queries.GetAllHotels
{
    public class GetAllHotelsListQueryHandler(ILogger<GetAllHotelsListQueryHandler> _logger
        , IMapper mapper
        , IHotelRepository _hotelRepository) : IRequestHandler<GetAllHotelsListQuery, IEnumerable<HotelDto>>
    {
        public async Task<IEnumerable<HotelDto>> Handle(GetAllHotelsListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllHotelsListQuery");

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Cancellation requested for GetAllHotelsListQuery");
                    cancellationToken.ThrowIfCancellationRequested();
                }

                var hotels = await _hotelRepository.GetAllHotelsAsync();
                if (hotels == null || !hotels.Any())
                {
                    _logger.LogWarning("No hotels found");
                    return new List<HotelDto>();
                }

                var hotelDtos = mapper.Map<IEnumerable<HotelDto>>(hotels);
                _logger.LogInformation("Successfully retrieved and mapped hotels");

                return hotelDtos;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Operation was cancelled for GetAllHotelsListQuery");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling GetAllHotelsListQuery");
                throw new ApplicationException("An unexpected error occurred while retrieving hotels", ex);
            }
        }
    }
}
