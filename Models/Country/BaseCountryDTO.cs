using System.ComponentModel.DataAnnotations;

namespace HotelHosting.Models.Country
    {
    public abstract class BaseCountryDTO
        {
        [Required]
        public string Name { get; set; }
        public string CountryCode { get; set; }
        }

    }
