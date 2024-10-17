using FiftyByte_POC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiftyByte_POC.Services.Interface
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country> GetCountryByIdAsync(int id);
        Task<Country> AddCountryAsync(Country country);
        Task<bool> UpdateCountryAsync(Country country);
        Task<bool> DeleteCountryAsync(int id);
    }
}
