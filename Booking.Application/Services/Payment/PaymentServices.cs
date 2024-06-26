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

public class PaymentServices : IPaymentServices
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<PaymentServices> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly ICurrentUserService _currentUserService;
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly IReservationRepository _reservationRepository;


    public PaymentServices(IConfiguration configuration, ILogger<PaymentServices> logger,
        ICurrentUserService currentUserService,
        IClientRepository clientRepository,
        IMapper mapper, IReservationRepository reservationRepository,
        IMemoryCache memoryCache)
    {
        _configuration = configuration;
        _logger = logger;
        _currentUserService = currentUserService;
        _clientRepository = clientRepository;
        _mapper = mapper;
        _reservationRepository = reservationRepository;
        _memoryCache = memoryCache;
    }

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

    //public async Task<string> Success(string sessionId)
    //{
    //    var session = await new SessionService().GetAsync(sessionId);
    //    var clientId = session.Metadata["ClientId"];
    //    var roomId = session.Metadata["RoomId"];


    //    var reservation = _mapper.Map<Reservation>(command);
    //    var result = await _reservationRepository.CreateAsync(reservation);
    //    if (result > 0)
    //        return "Succeeded";
    //    return "Failed";
    //}
    //{
    //    try
    //    {
    //        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

    //        var options = new PaymentIntentCreateOptions
    //        {
    //            Amount = (long)(amount * 100), // Convert amount to cents
    //            Currency = "USD",
    //        };

    //        var service = new PaymentIntentService();
    //        var paymentIntent = await service.CreateAsync(options);

    //        return paymentIntent.Id;
    //    }
    //    catch (StripeException ex)
    //    {
    //        _logger.LogError(ex, "Error creating PaymentIntent");
    //        throw;
    //    }
    //}

    //public async Task<bool> ConfirmPaymentIntent(string paymentIntentId, string paymentMethodId)
    //{
    //    try
    //    {
    //        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

    //        var service = new PaymentIntentService();
    //        var confirmOptions = new PaymentIntentConfirmOptions
    //        {
    //            PaymentMethod = paymentMethodId,
    //        };

    //        var confirmedIntent = await service.ConfirmAsync(paymentIntentId, confirmOptions);

    //        return confirmedIntent.Status == "succeeded";
    //    }
    //    catch (StripeException ex)
    //    {
    //        _logger.LogError(ex, "Error confirming PaymentIntent");
    //        throw;
    //    }
    //}

    //public async Task<bool> CapturePayment(string paymentIntentId)
    //{
    //    try
    //    {
    //        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

    //        var service = new PaymentIntentService();
    //        var captureOptions = new PaymentIntentCaptureOptions();
    //        var capturedIntent = await service.CaptureAsync(paymentIntentId, captureOptions);

    //        return capturedIntent.Status == "succeeded";
    //    }
    //    catch (StripeException ex)
    //    {
    //        _logger.LogError(ex, "Error capturing PaymentIntent");
    //        throw;
    //    }
    //}

    //public async Task<bool> RefundPayment(string paymentIntentId, decimal amountToRefund)
    //{
    //    try
    //    {
    //        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

    //        var refundService = new RefundService();
    //        var refundOptions = new RefundCreateOptions
    //        {
    //            PaymentIntent = paymentIntentId,
    //            Amount = (long)(amountToRefund * 100), // Convert amount to cents
    //        };

    //        var refund = await refundService.CreateAsync(refundOptions);

    //        return refund.Status == "succeeded";
    //    }
    //    catch (StripeException ex)
    //    {
    //        _logger.LogError(ex, "Stripe error refunding payment");
    //        throw;
    //    }
    //}
}
