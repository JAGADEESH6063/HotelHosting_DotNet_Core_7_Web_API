using HotelHosting.Models.Hotel;

namespace HotelHosting.Models.Country
{
    public class GetCountryByIdDTO : BaseCountryDTO
    {
        public int Id { get; set; }
        public List<HotelDTO> Hotels { get; set; } //One to Many Relationship
    }
}