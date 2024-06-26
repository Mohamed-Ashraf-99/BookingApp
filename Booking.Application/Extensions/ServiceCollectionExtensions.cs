using Booking.Application.Services.Authentication;
using Booking.Application.Services.Authorization;
using Booking.Application.Services.CurrentUser;
using Booking.Application.Services.Email;
using Booking.Application.Services.Payment;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Implementations;

namespace Booking.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddAutoMapper(applicationAssembly);

        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        services.AddMediatR(cfg
            => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddScoped<IAuthenticationServices, AuthenticationServices>();
        services.AddScoped<IAuthorizationServices, AuthorizationServices>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IPaymentServices, PaymentServices>();
        services.AddMemoryCache();

    }
}
