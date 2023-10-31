using System.ComponentModel.DataAnnotations.Schema;

namespace HotelHosting.Data
    {
    public class Country
        {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }

        public virtual IList<Hotel> Hotels { get; set; } //One to Many Relationship
        }
    }