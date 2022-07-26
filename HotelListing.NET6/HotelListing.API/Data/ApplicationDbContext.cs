using HotelListing.API.Data.Seeds;
using HotelListing.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApiUser>
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
            base.OnModelCreating(modelBuilder);

            //fromas de agregar un seed
            modelBuilder.ApplyConfiguration(new SeedRoleConfiguraion());

            seedCountry.SeedCountries(modelBuilder);
            seedHotel.SeedHotels(modelBuilder);

        }
    }
}
