using Booking.Application.Authentication.Helpers;
using Booking.Application.Emails.Helpers;
using Booking.Application.Services.ApplicationUser;
using Booking.Application.Services.Authentication;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Booking.Infrastructure.Repositories;
using Booking.Infrastructure.Seeders;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolProject.Service.Implementations;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Configuration;
using System.Text;


public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");

        services.AddDbContext<BookingDbContext>(options =>
            options.UseSqlServer(connectionString)
                   .EnableSensitiveDataLogging());

        // Add HangFire Configurations
        services.AddHangfire(config =>
            config.UseSqlServerStorage(connectionString, new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                UseRecommendedIsolationLevel = true,
                QueuePollInterval = TimeSpan.FromMinutes(5) // Check the queue every 5 minutes
            }));
        services.AddHangfireServer();

        #region JWT Configuration
        // Registering the Identity services with custom User and Role entities
        services.AddIdentity<User, IdentityRole<int>>(options =>
        {
            // Password settings 
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;

            // Lockout settings 
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings 
            options.User.RequireUniqueEmail = true;

            // Sign-in settings 
            options.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<BookingDbContext>()
        .AddDefaultTokenProviders();


        JwtSettings jwtSettings = new JwtSettings();
        //EmailSettings emailSettings = new EmailSettings();

        configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
        //configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);

        services.Configure<EmailSettings>(configuration.GetSection("emailSettings"));

        services.AddSingleton(jwtSettings);
        services.AddSingleton<EmailSettings>();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
       .AddJwtBearer(x =>
       {
           x.SaveToken = true;
           x.RequireHttpsMetadata = false;
           x.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = jwtSettings.ValidateIssuer,
               ValidIssuers = new[] { jwtSettings.Issuer },
               ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                       .GetBytes(jwtSettings.Secret)),
               ValidAudience = jwtSettings.Audience,
               ValidateAudience = jwtSettings.ValidateAudience,
               ValidateLifetime = jwtSettings.ValidateLifeTime,
           };
       });

        services.AddAuthorization();
        #endregion

        // Swagger Configuration
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking Project", Version = "v1" });
            c.EnableAnnotations();

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });

         
        });

        services.AddScoped<IBookingSeeder, BookingSeeder>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IWishListRepository, WishListRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<IReviewsRepository, ReviewsRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IComplainsRepository, ComplainsRepository>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<IAuthenticationServices, AuthenticationServices>();
        services.AddScoped<IOwnerRepository, OwnerRepository>();

    }
}
