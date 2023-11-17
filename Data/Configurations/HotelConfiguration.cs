using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelHosting.Data.Configurations
    {
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
        {
        public void Configure(EntityTypeBuilder<Hotel> builder)
            {
            builder.HasData(
                new Hotel { Id = 1, Address = "Anantapur", CountryId = 1, HotelName = "Masineni Hotel", Rating = 2.0 },
                new Hotel { Id = 2, Address = "Bangalore", CountryId = 1, HotelName = "Meghana Foods", Rating = 4.5 },
                new Hotel { Id = 3, Address = "New Jersey", CountryId = 2, HotelName = "Burger King", Rating = 3.0 },
                new Hotel { Id = 4, Address = "Kong Street", CountryId = 3, HotelName = "Noodles Hotel", Rating = 3.2 },
                new Hotel { Id = 5, Address = "Church Street", CountryId = 2, HotelName = "KFC", Rating = 1.0 }
                );
            }
        }

    }
