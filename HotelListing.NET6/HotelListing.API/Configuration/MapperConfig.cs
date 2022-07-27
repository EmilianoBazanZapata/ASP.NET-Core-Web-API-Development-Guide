using AutoMapper;
using HotelListing.API.Models;
using HotelListing.API.Models.CountryDTOS;
using HotelListing.API.Models.HotelDTOS;
using HotelListing.API.Models.UserDTOS;

namespace HotelListing.API.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country,CreateCountryDTO>().ReverseMap();
            CreateMap<Country, GetCountryDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();

            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();

            CreateMap<ApiUser, ApiUserDTO>().ReverseMap();
        }
    }
}
