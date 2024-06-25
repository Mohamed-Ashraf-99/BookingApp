using Booking.Application.Services.Payment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using System;
using System.Threading.Tasks;

public class PaymentServices : IPaymentServices
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<PaymentServices> _logger;

    public PaymentServices(IConfiguration configuration, ILogger<PaymentServices> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string> CreatePaymentIntent(decimal? amount)
    {
        try
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Convert amount to cents
                Currency = "USD",
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return paymentIntent.Id;
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Error creating PaymentIntent");
            throw;
        }
    }

    public async Task<bool> ConfirmPaymentIntent(string paymentIntentId, string paymentMethodId)
    {
        try
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var service = new PaymentIntentService();
            var confirmOptions = new PaymentIntentConfirmOptions
            {
                PaymentMethod = paymentMethodId,
            };

            var confirmedIntent = await service.ConfirmAsync(paymentIntentId, confirmOptions);

            return confirmedIntent.Status == "succeeded";
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Error confirming PaymentIntent");
            throw;
        }
    }

    public async Task<bool> CapturePayment(string paymentIntentId)
    {
        try
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var service = new PaymentIntentService();
            var captureOptions = new PaymentIntentCaptureOptions();
            var capturedIntent = await service.CaptureAsync(paymentIntentId, captureOptions);

            return capturedIntent.Status == "succeeded";
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Error capturing PaymentIntent");
            throw;
        }
    }

    public async Task<bool> RefundPayment(string paymentIntentId, decimal amountToRefund)
    {
        try
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var refundService = new RefundService();
            var refundOptions = new RefundCreateOptions
            {
                PaymentIntent = paymentIntentId,
                Amount = (long)(amountToRefund * 100), // Convert amount to cents
            };

            var refund = await refundService.CreateAsync(refundOptions);

            return refund.Status == "succeeded";
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Stripe error refunding payment");
            throw;
        }
    }
}
