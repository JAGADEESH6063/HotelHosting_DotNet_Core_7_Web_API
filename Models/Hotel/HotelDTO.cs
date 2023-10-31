namespace HotelHosting.Models.Hotel
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public int CountryId { get; set; }

    }
}