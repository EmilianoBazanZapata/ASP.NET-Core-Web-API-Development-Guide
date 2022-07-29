using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.UserDTOS
{

    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

}
