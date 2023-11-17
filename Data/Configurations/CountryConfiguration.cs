using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHosting.Data.Configurations
    {
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
        {
        public void Configure(EntityTypeBuilder<Country> builder)
            {
            builder.HasData(
                new Country { Id = 1, Name = "India", CountryCode = "IN" },
                new Country { Id = 2, Name = "America", CountryCode = "USA" },
                new Country { Id = 3, Name = "Japan", CountryCode = "JP" }
                );
            }
        }

    }
