using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Models;
using HotelListing.API.Models.CountryDTOS;
using AutoMapper;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Exceptions;
using HotelListing.API.Services;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly CountryService _countryService;

        public CountriesController(CountryService countryService)
        {
            _countryService = countryService;   
        }

        // GET: api/Countries
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PagedResult<GetCountryDTO>>> GetPagedCountries([FromQuery] QueryParameters queryParameters)
        {
            var countries = await _countryService.GetAllCountriesPaginated(queryParameters);
            return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id)
        {
            var country = await _countryService.GetCountryById(id);
            return Ok(country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateCountryDTO)
        {
            await _countryService.UpdateCountry(id, updateCountryDTO);
            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO createCountry)
        {
            var country = await _countryService.AddCountry(createCountry);
            return Ok();
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _countryService.DeleteCountry(id);    
            return NoContent();
        }
    }
}