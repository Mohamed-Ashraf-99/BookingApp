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

namespace Booking.Application.ApplicationUser.Queries.GetOffersByHotelId
{
    public class GetOffersByHotelIdListQueryHandler(ILogger<GetOffersByHotelIdListQueryHandler> _logger
        , IMapper mapper
        , IOfferRepository _offerRepository) : IRequestHandler<GetOffersByHotelIdListQuery, IEnumerable<OfferDto>>
    {
        public async Task<IEnumerable<OfferDto>> Handle(GetOffersByHotelIdListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetOffersByHotelIdListQuery");

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Cancellation requested for GetOffersByHotelIdListQuery");
                    cancellationToken.ThrowIfCancellationRequested();
                }

                var offers = await _offerRepository.GetOffersByHotelIdAsync(request.HotelId);
                if (offers == null)
                {
                    _logger.LogWarning("No offers found for HotelId {HotelId}", request.HotelId);
                    return new List<OfferDto>();
                }

                var offerDtos = mapper.Map<IEnumerable<OfferDto>>(offers);
                _logger.LogInformation("Successfully retrieved and mapped offers for HotelId {HotelId}", request.HotelId);

                return offerDtos;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Operation was cancelled for GetOffersByHotelIdListQuery");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling GetOffersByHotelIdListQuery");
                throw new ApplicationException("An unexpected error occurred while retrieving hotel offers", ex);
            }
        }
    }
}
