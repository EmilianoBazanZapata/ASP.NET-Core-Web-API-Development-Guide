using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Exceptions;
using HotelListing.API.Models;
using HotelListing.API.Models.CountryDTOS;

namespace HotelListing.API.Services
{
    public class CountryService
    {
        private readonly ICountriesRepository _countriesRepository;
        private readonly ILogger<CountryService> _logger;
        private readonly IMapper _mapper;

        public CountryService(ICountriesRepository countriesRepository,
                              ILogger<CountryService> logger,
                              IMapper mapper)
        {
            _countriesRepository = countriesRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PagedResult<GetCountryDTO>> GetAllCountriesPaginated(QueryParameters queryParameters)
        {
            var countries = await _countriesRepository.GetAllAsync<GetCountryDTO>(queryParameters);

            return countries;
        }

        public async Task<CountryDTO> GetCountryById(int id)
        {
            var country = await _countriesRepository.GetDetails(id);

            if (country == null)
                throw new NotFoundException(nameof(GetCountryById), id);

            var countryDTO = _mapper.Map<CountryDTO>(country);

            return countryDTO;
        }

        public async Task<object> AddCountry(CreateCountryDTO createCountry)
        {
            var country = _mapper.Map<Country>(createCountry);

            await _countriesRepository.AddAsync(country);

            return null;
        }

        public async Task<object> UpdateCountry(int id, UpdateCountryDTO updateCountryDTO)
        {
            var exists = await CountryExists(id);

            if (!exists)
                throw new NotFoundException(nameof(UpdateCountry), id);

            await _countriesRepository.UpdateAsync(id, updateCountryDTO);

            return null;
        }

        public async Task<object> DeleteCountry(int id) 
        {
            var country = await _countriesRepository.GetAsync(id);

            if (country == null)
                throw new NotFoundException(nameof(DeleteCountry), id);

            await _countriesRepository.DeleteAsync(id);

            return null;
        }

        public async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
