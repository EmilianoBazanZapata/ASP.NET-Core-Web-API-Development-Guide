using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Exceptions;
using HotelListing.API.Models;
using HotelListing.API.Models.HotelDTOS;

namespace HotelListing.API.Services
{
    public class HotelService
    {
        private readonly IHotelsRepository _hotelsRepository;
        private readonly ILogger<HotelService> _logger;
        private readonly IMapper _mapper;

        public HotelService(IHotelsRepository hotelsRepository,
                              ILogger<HotelService> logger,
                              IMapper mapper)
        {
            _hotelsRepository = hotelsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PagedResult<GetHotelDTO>> GetAllHotelsPaginated(QueryParameters queryParameters)
        {
            var hotels = await _hotelsRepository.GetAllAsync<GetHotelDTO>(queryParameters);
            return hotels;
        }

        public async Task<HotelDTO> GetHotelById(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);

            if (hotel == null)
                throw new NotFoundException(nameof(GetHotelById), id);

            var hotelDTO = _mapper.Map<HotelDTO>(hotel);

            return hotelDTO;
        }

        public async Task<ResponseHotelDTO> UpdateHotel(int id, UpdateHotelDTO updateHotelDTO)
        {
            var exists = await HotelExists(id);

            if (!exists)
                throw new NotFoundException(nameof(UpdateHotel), id);

            await _hotelsRepository.UpdateAsync(id, updateHotelDTO);

            return new ResponseHotelDTO 
            {
                Title = "Success",
                Body = $"The Hotel has been a Updated"
            };
        }

        public async Task<ResponseHotelDTO> AddHotel(CreateHotelDTO createHotelDTO)
        {
            var hotel = _mapper.Map<Hotel>(createHotelDTO);

            await _hotelsRepository.AddAsync(hotel);

            return new ResponseHotelDTO
            {
                Title = "Success",
                Body = $"The Hotel {createHotelDTO.Name} has been a Created"
            };
        }

        public async Task<ResponseHotelDTO> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);

            if (hotel == null)
                throw new NotFoundException(nameof(DeleteHotel), id);

            await _hotelsRepository.DeleteAsync(id);

            return new ResponseHotelDTO
            {
                Title = "Success",
                Body = $"The Hotel has been a Deleted"
            };
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
