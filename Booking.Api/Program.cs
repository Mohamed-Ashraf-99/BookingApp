using Booking.Infrastructure.Persistence;
using Booking.Api.Middlewares;
using Booking.Application.Extensions;
using Booking.Domain.Entities.Identity;
using Booking.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Microsoft.AspNetCore.Hosting;

namespace Booking.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register custom middleware
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

        // Register application and infrastructure services
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);


        // Registering the Identity services with custom User and Role entities
        builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
        {
            // Require a confirmed email to log in
            //options.SignIn.RequireConfirmedEmail = true;

            // Password settings (customize as needed)
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;

            // Lockout settings (customize as needed)
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings (customize as needed)
            options.User.RequireUniqueEmail = true;

            // Sign-in settings (customize as needed)
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
        .AddEntityFrameworkStores<BookingDbContext>() // Registering the DbContext for Identity
        .AddDefaultTokenProviders();

      
        // Configure Serilog
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration);
        });

        var app = builder.Build();

        // Seed data
        //using (var scope = app.Services.CreateScope())
        //{
        //    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
        //    seeder.Seed();
        //}

        // Use custom Middleware
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<RequestTimeLoggingMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
