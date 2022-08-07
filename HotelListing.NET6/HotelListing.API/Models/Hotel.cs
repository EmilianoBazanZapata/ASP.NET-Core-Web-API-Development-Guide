using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The name is to Long")]
        [MinLength(2, ErrorMessage = "The name is to Short")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "The address is to Long")]
        [MinLength(2, ErrorMessage = "The address is to Short")]
        public string Address { get; set; } = string.Empty;

        [Range(1,5,ErrorMessage = "The range is Invalid")]
        public double Rating { get; set; }

        [ForeignKey(nameof(CountryId))]
        public int CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
