using System.ComponentModel.DataAnnotations.Schema;

namespace HotelHosting.Data
    {
    public class Hotel
        {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string Address{ get; set; }
        public double Rating { get; set; }

        [ForeignKey(nameof(CountryId))]
        public int CountryId { get; set; }
        public Country Country { get; set; } //OnetoOne relation ship
        }
    }
