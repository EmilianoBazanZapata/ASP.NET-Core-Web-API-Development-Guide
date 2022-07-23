using AutoMapper;
using HotelListing.API.Models;
using HotelListing.API.Models.CountryDTOS;

namespace HotelListing.API.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country,CreateCountryDTO>().ReverseMap();
        }
    }
}
