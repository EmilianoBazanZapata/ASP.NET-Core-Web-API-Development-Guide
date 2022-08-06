using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models;

namespace HotelListing.API.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        private readonly IMapper _mapper;

        public HotelsRepository(ApplicationDbContext applicationDbContext,IMapper mapper) : base(applicationDbContext,mapper)
        {
            _mapper = mapper;
        }
    }
}
