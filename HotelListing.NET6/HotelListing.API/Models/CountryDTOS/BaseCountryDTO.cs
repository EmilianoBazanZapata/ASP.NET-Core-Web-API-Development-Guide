using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.CountryDTOS
{
    public abstract class BaseCountryDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "The name is to Long")]
        [MinLength(2, ErrorMessage = "The name is to Short")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "The short name is to Long")]
        public string ShortName { get; set; } = string.Empty;
    }
}
