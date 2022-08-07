using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.HotelDTOS
{
    public class BaseHotelDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "The name is to Long")]
        [MinLength(2, ErrorMessage = "The name is to Short")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "The address is to Long")]
        [MinLength(2, ErrorMessage = "The address is to Short")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Range(1, 5, ErrorMessage = "The range is Invalid")]
        public double Rating { get; set; }
        public int CountryId { get; set; }
    }
}
