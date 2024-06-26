﻿using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Persistence;

public class BookingDbContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{

    public BookingDbContext(DbContextOptions<BookingDbContext> options)
        : base(options)
    {
    }

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
    public DbSet<HotelWishList> HotelWishLists { get; set; }
    public DbSet<UserRefreshToken> UsersRefreshTokens { get; set; }
    public DbSet<Images> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().OwnsOne(res => res.Address);
        builder.Entity<Hotel>().OwnsOne(res => res.Address);
        builder.Entity<Client>().OwnsOne(res => res.Address);

        // Configure the Offer-Owner relationship
        builder.Entity<Offer>()
            .HasOne(o => o.Owner)
            .WithMany(owner => owner.Offers)
            .HasForeignKey(o => o.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the Offer-Hotel relationship
        builder.Entity<Offer>()
            .HasOne(o => o.Hotel)
            .WithMany(h => h.Offers)
            .HasForeignKey(o => o.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the Owner-Complains relationship with SetNull
        builder.Entity<Complains>()
            .HasOne(c => c.Owner)
            .WithMany(o => o.Complains)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<HotelWishList>()
            .HasKey(hw => new { hw.HotelsId, hw.WishListsId }); // Composite key

        builder.Entity<HotelWishList>().ToTable("HotelWishList");

        builder.Entity<HotelWishList>()
            .HasOne(hw => hw.Hotel)
            .WithMany(h => h.HotelWishLists)
            .HasForeignKey(hw => hw.HotelsId);

        builder.Entity<HotelWishList>()
            .HasOne(hw => hw.WishList)
            .WithMany(w => w.HotelWishLists)
            .HasForeignKey(hw => hw.WishListsId);


        builder.Entity<User>(entity =>
        {
            entity.Property(e => e.Code)
                .HasConversion(
                    v => EncryptionHelper.Encrypt(v),
                    v => EncryptionHelper.Decrypt(v)
                );
        });

        base.OnModelCreating(builder);
    }
}
