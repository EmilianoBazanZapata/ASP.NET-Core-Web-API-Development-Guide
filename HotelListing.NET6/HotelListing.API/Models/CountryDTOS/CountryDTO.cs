using HotelListing.API.Models.HotelDTOS;

namespace HotelListing.API.Models.CountryDTOS
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;

        public List<HotelDTO>? Hotels { get; set; }
    }
}
