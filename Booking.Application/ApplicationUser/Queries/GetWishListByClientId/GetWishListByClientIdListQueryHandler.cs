using AutoMapper;
using Booking.Application.ApplicationUser.Queries.GetAllCityHotels.Dto;
using Booking.Application.ApplicationUser.Queries.GetAllHotelOffers.Dto;
using Booking.Application.ApplicationUser.Queries.GetOffersByHotelId;
using Booking.Application.ApplicationUser.Queries.GetWishListByClientId.Dto;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetWishListByClientId
{
    public class GetWishListByClientIdListQueryHandler(ILogger<GetOffersByHotelIdListQueryHandler> _logger
        , IMapper mapper
        , IWishListRepository _WishListRepository , IClientRepository clientRepository) : IRequestHandler<GetWishListByClientIdListQuery, ClientWishListDto>
    {
        public async Task<ClientWishListDto> Handle(GetWishListByClientIdListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetWishListByClientIdListQuery");

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Cancellation requested for GetWishListByClientIdListQuery");
                    cancellationToken.ThrowIfCancellationRequested();
                }
                request.userId = await clientRepository.GetClientIdByUserId(request.userId);
                // Retrieve the wishlist from the repository based on the client's Id
                var wishList = await _WishListRepository.GetWishListByClientIdAsync(request.userId);

                // Map the retrieved wishlist entity to WishListDto using AutoMapper
                var wishListDto = mapper.Map<ClientWishListDto>(wishList);

                _logger.LogInformation("Successfully retrieved and mapped wishlist");

                return wishListDto;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Operation was cancelled for GetWishListByClientIdListQuery");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling GetWishListByClientIdListQuery");
                throw new ApplicationException("An unexpected error occurred while retrieving wishlist", ex);
            }
        }
    }
}
