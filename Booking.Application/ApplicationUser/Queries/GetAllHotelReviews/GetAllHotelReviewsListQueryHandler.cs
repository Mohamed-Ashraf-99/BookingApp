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

namespace Booking.Application.ApplicationUser.Queries.GetAllHotelReviews
{
    public class GetAllHotelReviewsListQueryHandler(ILogger<GetAllHotelReviewsListQueryHandler> _logger
        , IMapper mapper
        , IReviewsRepository _reviewRepository) : IRequestHandler<GetAllHotelReviewsListQuery, IEnumerable<RevieewDto>>
    {
        public async Task<IEnumerable<RevieewDto>> Handle(GetAllHotelReviewsListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetAllHotelReviewsListQuery");

                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Cancellation requested for GetAllHotelReviewsListQuery");
                    cancellationToken.ThrowIfCancellationRequested();
                }

                var reviews = await _reviewRepository.GetReviewsByHotelId(request.HotelID);

                if (reviews == null || !reviews.Any())
                {
                    _logger.LogWarning("No reviews found");
                    return new List<RevieewDto>();
                }

                var reviewDtos = mapper.Map<IEnumerable<RevieewDto>>(reviews);
                _logger.LogInformation("Successfully retrieved and mapped reviews");

                return reviewDtos;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Operation was cancelled for GetAllReviewsListQuery");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling GetAllHotelReviewsListQuery");
                throw new ApplicationException("An unexpected error occurred while retrieving reviews", ex);
            }
        }
    }
}
