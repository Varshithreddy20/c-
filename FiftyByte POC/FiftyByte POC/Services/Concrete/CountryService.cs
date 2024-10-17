using FiftyByte_POC.Models;
using FiftyByte_POC.Repositories.Interface;
using FiftyByte_POC.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiftyByte_POC.Services.Concrete
{
    public class CountryService(ICountryRepository countryRepository) : ICountryService
    {
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await countryRepository.GetAllCountriesAsync();
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await countryRepository.GetCountryByIdAsync(id);
        }

        public async Task<Country> AddCountryAsync(Country country)
        {
            return await countryRepository.AddCountryAsync(country);
        }

        public async Task<bool> UpdateCountryAsync(Country country)
        {
            return await countryRepository.UpdateCountryAsync(country);
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            return await countryRepository.DeleteCountryAsync(id);
        }
    }
}
