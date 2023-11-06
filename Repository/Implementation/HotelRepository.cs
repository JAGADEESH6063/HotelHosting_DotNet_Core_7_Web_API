using HotelHosting.Data;
using HotelHosting.Repository.Interfaces;

namespace HotelHosting.Repository.Implementation
    {
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
        {
        public HotelRepository(HotelListingDbContext hotelListingDbContext) : base(hotelListingDbContext)
            {
            }
        }
    }
