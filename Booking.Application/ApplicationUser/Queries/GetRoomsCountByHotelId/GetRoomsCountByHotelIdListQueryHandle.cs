using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllReviews.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllReviews;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetRoomsCountByHotelId
{
    public class GetRoomsCountByHotelIdListQueryHandle(ILogger<GetRoomsCountByHotelIdListQueryHandle> _logger
        , IRoomRepository roomRepository) : IRequestHandler<GetRoomsCountByHotelIdListQuery, int>
    {
        public async Task<int> Handle(GetRoomsCountByHotelIdListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetRoomsCountByHotelIdListQueryHandle");

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Cancellation requested for GetRoomsCountByHotelIdListQueryHandle");
                    cancellationToken.ThrowIfCancellationRequested();
                }

                int RoomCount = await roomRepository.GetRoomCountInHotel(request.HotelId);

                if (RoomCount == null )
                {
                    _logger.LogWarning("No Rooms found");
                    return 0;
                }

               
                _logger.LogInformation("Room count Successfully retrieved");

                return RoomCount;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Operation was cancelled for GetRoomsCountByHotelIdListQueryHandle");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling GetRoomsCountByHotelIdListQueryHandle");
                throw new ApplicationException("An unexpected error occurred while retrieving Room Count", ex);
            }
        }
    }
}
