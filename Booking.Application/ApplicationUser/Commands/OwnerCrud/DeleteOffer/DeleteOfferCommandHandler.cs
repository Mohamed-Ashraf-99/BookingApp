using Booking.Application.ApplicationUser.Commands.OwnerCrud.DeleteOffer;
using Booking.Domain.Entities;
using Booking.Domain.Exceptions;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.ApplicationUser.Commands.DeleteOffer
{
    public class DeleteOfferCommandHandler(ILogger<DeleteOfferCommandHandler> _logger,
    IOfferRepository offerRepository) : IRequestHandler<DeleteOfferCommand, string>
    {
        public async Task<string> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting offer with ID: {OfferId}", request.OfferId);

                if (request.OfferId == 0)
                {
                    _logger.LogWarning("Offer with ID: {OfferId} not found", request.OfferId);
                    throw new NotFoundException(nameof(Offer), request.OfferId.ToString());
                }

                await offerRepository.DeleteOffer(request.OfferId);

                _logger.LogInformation("Offer with ID: {OfferId} deleted successfully", request.OfferId);
                return $"Offer with ID: {request.OfferId} deleted successfully";
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Offer with ID: {OfferId} not found", request.OfferId);
                return ex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting offer with ID: {OfferId}", request.OfferId);
                return $"An unexpected error occurred while deleting offer with ID: {request.OfferId}";
            }
        }
    }
}
