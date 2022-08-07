using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models
{
    public class ApiUser : IdentityUser
    {
        [Required]
        [MaxLength(50, ErrorMessage = "The name is to Long")]
        [MinLength(2, ErrorMessage = "The name is to Short")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "The last name is to Long")]
        [MinLength(2, ErrorMessage = "The last name is to Short")]
        public string LastName { get; set; } = string.Empty;
    }
}