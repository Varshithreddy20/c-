using FiftyByte_POC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiftyByte_POC.Repositories.Interface
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryByIdAsync(int id);
        Task<Country> AddCountryAsync(Country country);
        Task<bool> UpdateCountryAsync(Country country);
        Task<bool> DeleteCountryAsync(int id);
    }
}
