using System.ComponentModel.DataAnnotations;

namespace HotelHosting.Models.ApiUsers
    {
    public class ApiUserDTO : LoginDto
        {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        }
    }
