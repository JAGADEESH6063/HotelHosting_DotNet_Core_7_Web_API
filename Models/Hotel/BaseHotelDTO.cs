using System.ComponentModel.DataAnnotations;

namespace HotelHosting.Models.Hotel
{
    public class BaseHotelDTO
    {
        [Required]
        public string HotelName { get; set; }
        [Required]
        public string Address { get; set; }
        public double Rating { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; }
    }
}
