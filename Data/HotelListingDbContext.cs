using HotelHosting.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelHosting.Data
    {
    public class HotelListingDbContext : IdentityDbContext<ApiUser>
        {
        public HotelListingDbContext(DbContextOptions options) : base(options){ }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            }
        }
    }