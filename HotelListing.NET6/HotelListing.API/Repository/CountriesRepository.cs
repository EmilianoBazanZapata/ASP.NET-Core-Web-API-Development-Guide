using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CountriesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _applicationDbContext.Countries.Include(c => c.Hotels)
                                                        .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
