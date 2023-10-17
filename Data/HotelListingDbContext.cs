using Microsoft.EntityFrameworkCore;

namespace HotelHosting.Data
    {
    public class HotelListingDbContext : DbContext
        {
        public HotelListingDbContext(DbContextOptions options) : base(options){ }
        }
    }