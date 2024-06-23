using Booking.Domain.Entities.Identity;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Booking.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Booking.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");

        services.AddDbContext<BookingDbContext>(options
            => options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

        services.AddScoped<IHotelRepository,HotelRepository>();
        services.AddScoped<IOfferRepository,OfferRepository>(); 
        services.AddScoped<IReviewsRepository,ReviewsRepository>();
        // services.AddIdentityCore<User>()
        //.AddEntityFrameworkStores<RestaurantDbContext>();

        //services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        //services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        //services.AddScoped<IDishRepository, DishRepository>();
    }
}
