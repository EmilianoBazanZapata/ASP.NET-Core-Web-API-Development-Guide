using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage = "The name is to Long")]
        [MinLength(2,ErrorMessage = "The name is to Short")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "The short name is to Long")]
        public string ShortName { get; set; } = string.Empty;

        public virtual IList<Hotel>? Hotels { get; set; }
    }
}