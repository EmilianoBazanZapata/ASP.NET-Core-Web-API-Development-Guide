namespace HotelListing.API.Models.CountryDTOS
{
    public class GetCountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
    }
}
