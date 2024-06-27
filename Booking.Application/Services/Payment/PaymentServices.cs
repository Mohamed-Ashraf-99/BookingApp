using AutoMapper;
using Booking.Application.Booking;
using Booking.Application.Booking.Commands.CreateReservation;
using Booking.Application.Services.CurrentUser;
using Booking.Application.Services.Payment;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.Checkout;
using System;
using System.Threading.Tasks;

public class PaymentServices(IConfiguration _configuration, ILogger<PaymentServices> _logger,
    IMemoryCache _memoryCache, ICurrentUserService _currentUserService,
    IClientRepository _clientRepository, IMapper _mapper, IReservationRepository _reservationRepository) : IPaymentServices
{
   

    public async Task<string> CreateCheckoutSessionAsync(CreateSessionDto command)
    {
        var successUrl = command.SuccessUrl;
        var cancelUrl = command.CancelUrl;
        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                     PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = Convert.ToInt32(command.Amount) * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Booking"
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
            Metadata = new Dictionary<string, string>
            {
                { "ClientId", command.UserId.ToString() },
                { "RoomId", command.RoomId.ToString() }
            }
        };
        var service = new SessionService();
        Session session = await service.CreateAsync(options);

        _memoryCache.Set("SessionId", session.Id);
        _memoryCache.Set("ClientId", command.UserId);
        _memoryCache.Set("RoomId", command.RoomId);

        return session.Url;
    }

    public async Task<string> Success(CreateReservationCommand? command)
    {
        var user = await _currentUserService.GetUserAsync();
        var cleintId = await _clientRepository.GetClientIdByUserId(user.Id);
        command.ClientId = cleintId;
        var reservation = _mapper.Map<Reservation>(command);
        var result = await _reservationRepository.CreateAsync(reservation);
        if (result > 0)
            return "Succeeded";
        return "Failed";
    }
}
