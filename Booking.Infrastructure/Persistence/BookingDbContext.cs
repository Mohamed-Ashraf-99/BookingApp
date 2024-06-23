using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Booking.Infrastructure.Persistence;

public class BookingDbContext(DbContextOptions options) : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>(options)
{

    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Complains> Complains { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Meals> Meals { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Owner> Owner { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<PackageFacilities> PackageFacilities { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomFacilities> RoomFacilities { get; set; }
    public DbSet<WishList> WishList { get; set; }
    public DbSet<Images> images { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
           .OwnsOne(res => res.Address);

        builder.Entity<Hotel>()
         .OwnsOne(res => res.Address);

        builder.Entity<Client>()
         .OwnsOne(res => res.Address);

        base.OnModelCreating(builder);
    }


}
