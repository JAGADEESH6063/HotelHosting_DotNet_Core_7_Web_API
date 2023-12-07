using AutoMapper;
using HotelHosting.Data;
using HotelHosting.Models.ApiUsers;
using HotelHosting.Models.Country;
using HotelHosting.Models.Hotel;

namespace HotelHosting.Configurations
    {
    public class AutoMapperConfig : Profile
        {
        public AutoMapperConfig() 
            {
            CreateMap<Country,CountryDTO>().ReverseMap();
            CreateMap<Country, GetCountryDTO>().ReverseMap();
            CreateMap<Country, GetCountryByIdDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();


            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();

            CreateMap<ApiUser, ApiUserDTO>().ReverseMap();

            }
        }
    }
