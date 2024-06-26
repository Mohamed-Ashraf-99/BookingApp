﻿using Booking.Application.Services;
using Booking.Application.Services.ApplicationUser;
using Booking.Application.Services.Authentication;
using Booking.Application.Services.Authorization;
using Booking.Application.Services.ClientProfile;
using Booking.Application.Services.CurrentUser;
using Booking.Application.Services.Email;
using Booking.Application.Services.FileUpload;
using Booking.Application.Services.OwnerServ;
using Booking.Application.Services.Payment;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddScoped<IAuthorizationServices, AuthorizationServices>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IPaymentServices, PaymentServices>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<IClientProfileService, ClientProfileService>();
        services.AddMemoryCache();

    }
}
