using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.CountryDTOS
{
    public class CreateCountryDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
    }
}
