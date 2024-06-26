﻿using Booking.Application.Booking;
using Booking.Application.Booking.Commands.CreateReservation;

namespace Booking.Application.Services.Payment;

public interface IPaymentServices
{
    Task<string> CreateCheckoutSessionAsync(CreateSessionDto command);
    Task<string> Success(CreateReservationCommand? command);
    //Task<string> CreatePaymentIntent(decimal? amount);
    //Task<bool> CapturePayment(string paymentIntentId);
    //Task<bool> RefundPayment(string paymentIntentId, decimal amountToRefund);
    //Task<bool> ConfirmPaymentIntent(string paymentIntentId, string paymentMethodId);
}
