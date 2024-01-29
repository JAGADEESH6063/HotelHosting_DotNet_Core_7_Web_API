using AutoMapper;
using HotelHosting.Data;
using HotelHosting.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelHosting.Repository.Implementation
    {
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
        {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public CountryRepository(HotelListingDbContext hotelListingDbContext, IMapper mapper) : base(hotelListingDbContext, mapper)
            {
            _context = hotelListingDbContext;
            _mapper = mapper;
            }

        public async Task<Country> GetCountryWithHotelsList(int id)
            {
            var countryWithHotels = await _context.Countries.Include(q => q.Hotels)
                .FirstOrDefaultAsync(q => q.Id == id);
            //Default means it will return null if don't find a record in DB
            return countryWithHotels;
            }
        }
    }
