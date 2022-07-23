using HotelListing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data.Seeds
{
    public class SeedCountry
    {
        public void SeedCountries(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name ="Jamaica",
                    ShortName = "JM"
                }, 
                new Country
                {
                    Id = 2,
                    Name = "Bahaman",
                    ShortName = "BS"
                },
                new Country
                {
                    Id = 3,
                    Name = "Cayman Island",
                    ShortName = "CI"
                }
            );

        }
    }
}
