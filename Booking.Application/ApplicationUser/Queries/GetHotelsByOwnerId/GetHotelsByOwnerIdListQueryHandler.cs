using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetHotelById;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetHotelsByOwnerId
{
    public class GetHotelsByOwnerIdListQueryHandler(ILogger<GetHotelsByOwnerIdListQueryHandler> _logger,
    IMapper _mapper,
    IHotelRepository _hotelRepository , IOwnerRepository ownerRepository) : IRequestHandler<GetHotelsByOwnerIdListQuery, IEnumerable<HotelDto>>
    {


        public async Task<IEnumerable<HotelDto>> Handle(GetHotelsByOwnerIdListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                request.UserId = await ownerRepository.GetOwnerIdByUserId(request.UserId);
                _logger.LogInformation($"Handling GetHotelsByOwnerIdListQuery for OwnerId {request.UserId}");

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Cancellation requested for GetHotelsByOwnerIdListQuery");
                    cancellationToken.ThrowIfCancellationRequested();
                }
                
                var hotels = await _hotelRepository.GetHotelByOwnerIdAsync(request.UserId);
                if (hotels == null || !hotels.Any())
                {
                    _logger.LogWarning($"No hotels found for OwnerId {request.UserId}");
                    return new List<HotelDto>();
                }

                var hotelDtos = _mapper.Map<IEnumerable<HotelDto>>(hotels);
                _logger.LogInformation($"Successfully retrieved and mapped hotels for OwnerId {request.UserId}");

                return hotelDtos;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Operation was cancelled for GetHotelsByOwnerIdListQuery");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while handling GetHotelsByOwnerIdListQuery for OwnerId {request.UserId}");
                throw new ApplicationException("An unexpected error occurred while retrieving hotels by owner", ex);
            }
        }
    }
}
