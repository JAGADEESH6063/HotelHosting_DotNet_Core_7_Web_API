using Microsoft.EntityFrameworkCore;

namespace HotelHosting.Data
    {
    public class HotelListingDbContext : DbContext
        {
        public HotelListingDbContext(DbContextOptions options) : base(options){ }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1,Name="India",CountryCode="IN"},
                new Country { Id = 2, Name = "America", CountryCode = "USA" },
                new Country { Id = 3, Name = "Japan", CountryCode = "JP" }
                );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1,Address="Anantapur", CountryId = 1, HotelName = "Masineni Hotel", Rating = 2.0 },
                new Hotel { Id = 2, Address = "Bangalore", CountryId = 1, HotelName = "Meghana Foods", Rating = 4.5 },
                new Hotel { Id = 3, Address = "New Jersey", CountryId = 2, HotelName = "Burger King", Rating = 3.0 },
                new Hotel { Id = 4, Address = "Kong Street", CountryId = 3, HotelName = "Noodles Hotel", Rating = 3.2 },
                new Hotel { Id = 5, Address = "Church Street", CountryId = 2, HotelName = "KFC", Rating = 1.0 }
                );
            }
        }
    }