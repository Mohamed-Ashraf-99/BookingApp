using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllHotelOffers
{
    public class GetAllHotelOffersListQueryHandler(ILogger<GetAllHotelOffersListQueryHandler> _logger
        , IMapper mapper
        , IOfferRepository _offerRepository) : IRequestHandler<GetAllHotelOffersListQuery, IEnumerable<OfferDto>>
    {
        public async Task<IEnumerable<OfferDto>> Handle(GetAllHotelOffersListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllOffersListQuery");

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Cancellation requested for GetAllHotelOffersListQuery");
                    cancellationToken.ThrowIfCancellationRequested();
                }

                var offers = await _offerRepository.GetAllOffersAsync();
                if (offers == null)
                {
                    _logger.LogWarning("No offers found");
                    return new List<OfferDto>();
                }

                var offerDtos = mapper.Map<IEnumerable<OfferDto>>(offers);
                _logger.LogInformation("Successfully retrieved and mapped offers");

                return offerDtos;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Operation was cancelled for GetAllHotelOffersListQuery");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling GetAllHotelOffersListQuery");
                throw new ApplicationException("An unexpected error occurred while retrieving hotel offers", ex);
            }
        }
    }
}
