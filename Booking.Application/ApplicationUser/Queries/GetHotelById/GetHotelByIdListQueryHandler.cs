using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetUserById.Dto;
using Booking.Application.ApplicationUser.Queries.GetUserById;
using Booking.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Repositories;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;

namespace Booking.Application.ApplicationUser.Queries.GetHotelById
{
    internal class GetHotelByIdListQueryHandler(ILogger<GetHotelByIdListQueryHandler> _logger,
    IMapper _mapper,
    IHotelRepository _hotelRepository) : IRequestHandler<GetHotelByIdListQuery, HotelDto>
    {
        public async Task<HotelDto> Handle(GetHotelByIdListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling GetHotelByIdListQuery for HotelId {request.HotelId}");

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Cancellation requested for GetHotelByIdListQuery");
                    cancellationToken.ThrowIfCancellationRequested();
                }

                var hotel = await _hotelRepository.GetHotelByIdAsync(request.HotelId);
                if (hotel == null)
                {
                    _logger.LogWarning($"No hotel found with ID {request.HotelId}");
                    return null;
                }

                var hotelDto = _mapper.Map<HotelDto>(hotel);
                _logger.LogInformation($"Successfully retrieved and mapped hotel with ID {request.HotelId}");

                return hotelDto;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Operation was cancelled for GetHotelByIdListQuery");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while handling GetHotelByIdListQuery for HotelId {request.HotelId}");
                throw new ApplicationException("An unexpected error occurred while retrieving the hotel", ex);
            }
        }
    }
}
