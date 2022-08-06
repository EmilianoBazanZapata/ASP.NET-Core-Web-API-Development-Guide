using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models;
using HotelListing.API.Models.CountryDTOS;
using AutoMapper;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Exceptions;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _countriesRepository;
        private readonly ILogger<CountriesController> _logger;
        private readonly IMapper _mapper;

        public CountriesController(IMapper mapper, 
                                   ICountriesRepository countriesRepository,
                                   ILogger<CountriesController> logger)
        {
            _mapper = mapper;
            _countriesRepository = countriesRepository;
            _logger = logger;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<PagedResult<GetCountryDTO>>> GetPagedCountries([FromQuery] QueryParameters queryParameters)
        {
            var countries = await _countriesRepository.GetAllAsync<GetCountryDTO>(queryParameters);
            return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetails(id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetCountry), id);
               // _logger.LogWarning($"Record not found in {nameof(GetCountry)} with Id: {id}.");
               // return NotFound();
            }
            var countryDTO = _mapper.Map<CountryDTO>(country);
            return Ok(countryDTO);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateCountryDTO)
        {
            if (id != updateCountryDTO.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            var country = await _countriesRepository.GetAsync(id);

            if (country is null) 
            {
                return NotFound();
            }

            _mapper.Map(updateCountryDTO,country);

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO createCountry)
        {

            var country = _mapper.Map<Country>(createCountry);  

            await _countriesRepository.AddAsync(country);    

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}