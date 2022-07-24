using HotelListing.API.Models.HotelDTOS;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Models.CountryDTOS
{
    public class GetCountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
    }

    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;

        public List<HotelDTO>? Hotels { get; set; }
    }
}
