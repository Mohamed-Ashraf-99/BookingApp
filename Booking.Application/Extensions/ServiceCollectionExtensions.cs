using Booking.Application.Services.Authentication;
using Booking.Application.Services.Authorization;
using FluentValidation;
using FluentValidation.AspNetCore;
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

    }
}
