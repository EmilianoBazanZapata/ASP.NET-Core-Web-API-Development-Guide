using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Models;
using HotelListing.API.Models.HotelDTOS;
using HotelListing.API.Services;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly HotelService _hotelService;
        public HotelsController(HotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<PagedResult<GetHotelDTO>>> GetPagedHotels([FromQuery] QueryParameters queryParameters)
        {
            var hotels = await _hotelService.GetAllHotelsPaginated(queryParameters);
            return hotels;
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            var hotel = await _hotelService.GetHotelById(id);   
            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ResponseHotelDTO> PutHotel(int id, UpdateHotelDTO updateHotelDTO)
        {
            var result = await _hotelService.UpdateHotel(id, updateHotelDTO);
            return result;
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ResponseHotelDTO> PostHotel(CreateHotelDTO createHotelDTO)
        {
            var result = await _hotelService.AddHotel(createHotelDTO);
            return result;
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<ResponseHotelDTO> DeleteHotel(int id)
        {
            var result = await _hotelService.DeleteHotel(id);
            return result;
        }
    }
}
