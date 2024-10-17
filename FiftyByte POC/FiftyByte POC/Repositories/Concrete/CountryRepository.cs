using FiftyByte_POC.Data;
using FiftyByte_POC.Models;
using FiftyByte_POC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiftyByte_POC.Repositories.Concrete
{
    public class CountryRepository(ApplicationDbContext dbContext) : ICountryRepository
    {
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await dbContext.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await dbContext.Countries.FindAsync(id);
        }

        public async Task<Country> AddCountryAsync(Country country)
        {
            dbContext.Countries.Add(country);
            await dbContext.SaveChangesAsync();
            return country;
        }

        public async Task<bool> UpdateCountryAsync(Country country)
        {
            var existingCountry = await dbContext.Countries.FindAsync(country.CountryId);
            if (existingCountry == null) return false;

            existingCountry.CountryName = country.CountryName;
            existingCountry.CountryCode = country.CountryCode;

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            var country = await dbContext.Countries.FindAsync(id);
            if (country == null) return false;

            dbContext.Countries.Remove(country);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
