using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.UserDTOS
{
    public partial class ApiUserDTO : LoginDTO
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;
    }
}
