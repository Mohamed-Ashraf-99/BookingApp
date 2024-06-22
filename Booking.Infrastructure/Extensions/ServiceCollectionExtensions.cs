using Booking.Application.Authentication.Helpers;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Booking.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Booking.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");

        services.AddDbContext<BookingDbContext>(options
            => options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

        #region JWT Authentication
        var jwtSettings = new JwtSettings();
        configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
        services.AddSingleton(jwtSettings);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { jwtSettings.Issuer },
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSignInKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ValidAudience = jwtSettings.Audience,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidateLifetime = jwtSettings.ValidateLifeTime,
            };
        });
        #endregion

        #region RefreshToken
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        #endregion
    }
}
