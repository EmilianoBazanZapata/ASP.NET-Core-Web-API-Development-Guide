using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Exceptions;
using HotelListing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CountriesRepository(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext, mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Country> GetDetails(int id)
        {
            var country = await _applicationDbContext.Countries.Include(c => c.Hotels)
                                                        .FirstOrDefaultAsync(c => c.Id == id);

            if (country is null)
                throw new NotFoundException(nameof(GetDetails), id);

            return country;
        }
    }
}
