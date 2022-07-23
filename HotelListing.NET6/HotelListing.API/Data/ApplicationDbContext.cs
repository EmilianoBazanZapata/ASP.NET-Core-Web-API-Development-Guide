using HotelListing.API.Data.Seeds;
using HotelListing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }


        SeedCountry seedCountry = new();
        SeedHotel seedHotel = new();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            seedCountry.SeedCountries(modelBuilder);
            seedHotel.SeedHotels(modelBuilder);

        }
    }
}
