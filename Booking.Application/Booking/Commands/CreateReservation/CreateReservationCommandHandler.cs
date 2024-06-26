//using AutoMapper;
//using Booking.Application.Booking.Commands.CreateReservation;
//using Booking.Application.Services.CurrentUser;
//using Booking.Application.Services.Payment;
//using Booking.Domain.Entities.Enums;
//using Booking.Domain.Entities;
//using Booking.Domain.Repositories;
//using MediatR;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Stripe.Checkout;



//public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, string>
//{
//    private readonly ILogger<CreateReservationCommandHandler> _logger;
//    private readonly IMapper _mapper;
//    private readonly IReservationRepository _reservationRepository;
//    private readonly IRoomRepository _roomRepository;
//    private readonly ICurrentUserService _currentUserService;
//    private readonly IClientRepository _clientRepository;
//    private readonly IPaymentServices _paymentService;
//    private readonly IConfiguration _configuration;

//    public CreateReservationCommandHandler(
//        ILogger<CreateReservationCommandHandler> logger,
//        IMapper mapper,
//        IReservationRepository reservationRepository,
//        IRoomRepository roomRepository,
//        ICurrentUserService currentUserService,
//        IClientRepository clientRepository,
//        IPaymentServices paymentService,
//        IConfiguration configuration)
//    {
//        _logger = logger;
//        _mapper = mapper;
//        _reservationRepository = reservationRepository;
//        _roomRepository = roomRepository;
//        _currentUserService = currentUserService;
//        _clientRepository = clientRepository;
//        _paymentService = paymentService;
//        _configuration = configuration;
//    }

//    public async Task<string> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
//    {
//        _logger.LogInformation("Create a reservation");
//        var reservation = _mapper.Map<Reservation>(request);
//        // Map rooms to reservation
//        foreach (var roomId in request.RoomId)
//        {
//            var room = await _roomRepository.GetByIdAsync(roomId);
//            if (room != null)
//            {
//                //reservation.Rooms.Add(room);
//            }
//        }

//        // Set client ID
//        var userId = await _currentUserService.GetUserId();
//        var clientId = await _clientRepository.GetClientIdByUserId(userId);
//        reservation.ClientId = clientId;

//        // Create PaymentIntent if amount > 0
//        if (request.Amount > 0)
//        {
//            try
//            {
//                //reservation.PaymentIntentId = await _paymentService.CreatePaymentIntent(request.Amount);
//                reservation.Amount = request.Amount;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error processing payment");
//                return "Failed";
//            }
//        }

//        reservation.State = ReservationState.Confirmed;

//        // Save reservation
//        var response = await _reservationRepository.CreateAsync(reservation);

//        // If reservation creation is successful, initiate Stripe checkout session
//        if (response > 0)
//        {
//            var successUrl = "http://localhost:4200/payment-success"; // Replace with your actual success URL
//            var cancelUrl = "http://localhost:4200/payment-cancel";   // Replace with your actual cancel URL

//            // Create a Stripe Checkout Session
//            var options = new SessionCreateOptions
//            {
//                PaymentMethodTypes = new List<string> { "card" },
//                LineItems = new List<SessionLineItemOptions>
//                {
//                    new SessionLineItemOptions
//                    {
//                        PriceData = new SessionLineItemPriceDataOptions
//                        {
//                            UnitAmount = (long)(request.Amount * 100), // Convert amount to cents
//                            Currency = "usd",
//                            ProductData = new SessionLineItemPriceDataProductDataOptions
//                            {
//                                Name = "Reservation Payment",
//                            },
//                        },
//                        Quantity = 1,
//                    },
//                },
//                Mode = "payment",
//                SuccessUrl = successUrl,
//                CancelUrl = cancelUrl,
//            };

//            var service = new SessionService();
//            var session = await service.CreateAsync(options);

//            // Redirect to Stripe checkout page
//            return session.Url;
//        }
//        else
//        {
//            return "Failed";
//        }
//    }
//}
