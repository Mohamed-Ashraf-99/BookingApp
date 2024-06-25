using Booking.Domain.Entities;
using Booking.Domain.Entities.Enums;
using Booking.Domain.Entities.Identity;
using Booking.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Booking.Infrastructure.Seeders
{
    public class BookingSeeder : IBookingSeeder
    {
        private readonly BookingDbContext _context;

        public BookingSeeder(BookingDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Users.Any())
                {
                    var users = GetUsers();
                    _context.Users.AddRange(users);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Admins.Any())
                {
                    var admins = GetAdmins();
                    _context.Admins.AddRange(admins);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Clients.Any())
                {
                    var clients = GetClients();
                    _context.Clients.AddRange(clients);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Owner.Any())
                {
                    var owners = GetOwners();
                    _context.Owner.AddRange(owners);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Hotels.Any())
                {
                    var hotels = GetHotels();
                    _context.Hotels.AddRange(hotels);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _context.Restaurants.AddRange(restaurants);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Rooms.Any())
                {
                    var rooms = GetRooms();
                    _context.Rooms.AddRange(rooms);
                    await _context.SaveChangesAsync();
                }

                if (!_context.PackageFacilities.Any())
                {
                    var packageFacilities = GetPackageFacilities();
                    _context.PackageFacilities.AddRange(packageFacilities);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Packages.Any())
                {
                    var packages = GetPackages();
                    _context.Packages.AddRange(packages);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Meals.Any())
                {
                    var meals = GetMeals();
                    _context.Meals.AddRange(meals);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Offers.Any())
                {
                    var offers = GetOffers();
                    _context.Offers.AddRange(offers);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Complains.Any())
                {
                    var complains = GetComplains();
                    _context.Complains.AddRange(complains);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Reservations.Any())
                {
                    var reservations = GetReservations();
                    _context.Reservations.AddRange(reservations);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Reviews.Any())
                {
                    var reviews = GetReviews();
                    _context.Reviews.AddRange(reviews);
                    await _context.SaveChangesAsync();
                }

                if (!_context.WishList.Any())
                {
                    var wishLists = GetWishLists();
                    _context.WishList.AddRange(wishLists);
                    await _context.SaveChangesAsync();
                }

                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    await _context.SaveChangesAsync();
                }

            }
        }

        private IEnumerable<User> GetUsers()
        {
            return new List<User>
            {
                new User { UserName = "john_doe", Email = "john.doe@example.com" },
                new User { UserName = "jane_smith", Email = "jane.smith@example.com" },
                new User { UserName = "mike_jones", Email = "mike.jones@example.com" }
            };
        }

        private IEnumerable<Admin> GetAdmins()
        {
            return new List<Admin>
            {
                new Admin { User = _context.Users.Find(1) },
                new Admin { User = _context.Users.Find(2) }
            };
        }

        private IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client { User = _context.Users.Find(1), Address = new Address { City = "New York", Street = "5th Avenue", PostalCode = "10001" } },
                new Client { User = _context.Users.Find(2), Address = new Address { City = "Los Angeles", Street = "Sunset Boulevard", PostalCode = "90001" } }
            };
        }

        private IEnumerable<Owner> GetOwners()
        {
            return new List<Owner>
            {
                new Owner { User = _context.Users.Find(2), IsActive = true }
            };
        }

        private IEnumerable<Hotel> GetHotels()
        {
            return new List<Hotel>
            {
                new Hotel { Name = "The Plaza", Description = "Luxury hotel in New York", NumberOfStars = 5, Address = new Address { City = "New York", Street = "768 5th Ave", PostalCode = "10019" }, OwnerId = 1 },
                new Hotel { Name = "Beverly Hills Hotel", Description = "Iconic hotel in Beverly Hills", NumberOfStars = 5, Address = new Address { City = "Beverly Hills", Street = "9641 Sunset Blvd", PostalCode = "90210" }, OwnerId = 1 }
            };
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            return new List<Restaurant>
            {
                new Restaurant { Name = "The Palm Court", HotelId = 1, OwnerId = 1, IsDeleted = false, Cuisine = Cuisine.French, Ambience = Ambience.Formal, DietaryOptions = DietaryOptions.Vegetarian | DietaryOptions.GlutenFree, OpenFor = OpenFor.Breakfast | OpenFor.Lunch | OpenFor.Dinner },
                new Restaurant { Name = "Polo Lounge", HotelId = 2, OwnerId = 1, IsDeleted = false, Cuisine = Cuisine.American, Ambience = Ambience.Casual, DietaryOptions = DietaryOptions.None, OpenFor = OpenFor.Breakfast | OpenFor.Lunch | OpenFor.Dinner }
            };
        }

        private IEnumerable<Room> GetRooms()
        {
            return new List<Room>
            {
                new Room { RoomType = RoomType.Single, Description = "Single Room", Price = 500, NumberOfBeds = 1, HotelId = 1 },
                new Room { RoomType = RoomType.Double, Description = "Double Room", Price = 800, NumberOfBeds = 2, HotelId = 2 }
            };
        }

        private IEnumerable<PackageFacilities> GetPackageFacilities()
        {
            return new List<PackageFacilities>
            {
                new PackageFacilities { Name = "WiFi", Price = 15 },
                new PackageFacilities { Name = "Breakfast Included", Price = 20 }
            };
        }

        private IEnumerable<Package> GetPackages()
        {
            return new List<Package>
            {
                new Package { TotalPrice = 1000, PackageFacilities = _context.PackageFacilities.ToList() },
                new Package { TotalPrice = 1500, PackageFacilities = _context.PackageFacilities.ToList() }
            };
        }

        private IEnumerable<Meals> GetMeals()
        {
            return new List<Meals>
            {
                new Meals { Name = "Continental Breakfast", Price = 30 },
                new Meals { Name = "Buffet Dinner", Price = 50 }
            };
        }

        private IEnumerable<Offer> GetOffers()
        {
            return new List<Offer>
            {
                new Offer { Description = "Summer Sale", StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(10), Discount = 0.1m, HotelId = 1, IsDeleted = false, OwnerId = 1 },
                new Offer { Description = "Winter Special", StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddDays(20), Discount = 0.2m, HotelId = 2, IsDeleted = false, OwnerId = 1 }
            };
        }

        private IEnumerable<Complains> GetComplains()
        {
            return new List<Complains>
            {
                new Complains { Discription = "Room not clean", IsSolved = false, Date = DateTime.Now, HotelId = 1, AdminId = 1, ClientId = 1 },
                new Complains { Discription = "No hot water", IsSolved = true, Date = DateTime.Now, HotelId = 2, AdminId = 1, ClientId = 2 }
            };
        }

        private IEnumerable<Reservation> GetReservations()
        {
            return new List<Reservation>
            {
                new Reservation { StartDate = DateTime.Now.AddDays(5), EndDate = DateTime.Now.AddDays(10), NumberOfGuests = 1, IsDeleted = false, ClientId = 1, Amount = 500, State = ReservationState.Confirmed, Rooms = new List<Room> { _context.Rooms.Find(1) } },
                new Reservation { StartDate = DateTime.Now.AddDays(15), EndDate = DateTime.Now.AddDays(20), NumberOfGuests = 2, IsDeleted = false, ClientId = 2, Amount = 1600, State = ReservationState.Pending, Rooms = new List<Room> { _context.Rooms.Find(2) } }
            };
        }

        private IEnumerable<Reviews> GetReviews()
        {
            return new List<Reviews>
            {
                new Reviews {Rate = 5, Comment = "Excellent stay!", Date = DateTime.Now, HotelId = 1, ClientId = 1 },
                new Reviews {Rate = 4, Comment = "Very good experience.", Date = DateTime.Now, HotelId = 2, ClientId = 2 }
            };
        }

        private IEnumerable<WishList> GetWishLists()
        {
            return new List<WishList>
            {
                new WishList {  ClientId = 1 /*Hotels = new List<Hotel> { _context.Hotels.Find(1) }*/} ,
                new WishList {  ClientId = 2 /*Hotels = new List<Hotel> { _context.Hotels.Find(2) }*/}
            };
        }

        private IEnumerable<IdentityRole<int>> GetRoles()
        {
            return new List<IdentityRole<int>>
            {
              new IdentityRole<int>(){  Name = "Admin" },
              new IdentityRole<int>(){  Name = "User" },
              new IdentityRole<int>(){  Name = "Admin" },
            };
        }
    }
}
