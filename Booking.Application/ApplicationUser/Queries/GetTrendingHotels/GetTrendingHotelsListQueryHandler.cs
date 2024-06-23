using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllHotels;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetTrendingHotels
{
    public class GetTrendingHotelsListQueryHandler(ILogger<GetTrendingHotelsListQueryHandler> _logger
        , IMapper mapper
        , IHotelRepository _hotelRepository) : IRequestHandler<GetTrendingHotelsListQuery, IEnumerable<HotelDto>>
    {
        public async Task<IEnumerable<HotelDto>> Handle(GetTrendingHotelsListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetTrendingHotelsListQuery");

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Cancellation requested for GetTrendingHotelsListQuery");
                    cancellationToken.ThrowIfCancellationRequested();
                }

          
                // Get hotels with an average rating greater than the overall average rating
                var trendingHotels = await _hotelRepository.GetTrendingHotelsAsync();

                if (trendingHotels == null || !trendingHotels.Any())
                {
                    _logger.LogWarning("No trending hotels found");
                    return new List<HotelDto>();
                }

                var hotelDtos = mapper.Map<IEnumerable<HotelDto>>(trendingHotels);
                _logger.LogInformation("Successfully retrieved and mapped trending hotels");

                return hotelDtos;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Operation was cancelled for GetTrendingHotelsListQuery");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling GetTrendingHotelsListQuery");
                throw new ApplicationException("An unexpected error occurred while retrieving trending hotels", ex);
            }
        }
    }
}
