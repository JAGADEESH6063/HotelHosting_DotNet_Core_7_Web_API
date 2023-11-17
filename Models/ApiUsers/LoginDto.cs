using System.ComponentModel.DataAnnotations;

namespace HotelHosting.Models.ApiUsers
    {
    public class LoginDto 
        {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Password length should be 8 to 15 characters", MinimumLength = 8)]
        public string Password { get; set; }
        }
    }
