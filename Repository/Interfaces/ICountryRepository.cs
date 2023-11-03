using HotelHosting.Data;

namespace HotelHosting.Repository.Interfaces
    {
    public interface ICountryRepository : IGenericRepository<Country>
        {
            Task<Country> GetCountryWithHotelsList(int id);
        }
    }
