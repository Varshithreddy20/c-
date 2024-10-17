using FiftyByte_POC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiftyByte_POC.Repositories.Interface
{
    public interface ICompanyLogoRepository
    {
        Task<CompanyLogo> AddLogoAsync(CompanyLogo companyLogo);
        Task<CompanyLogo> GetLogoByIdAsync(int id);
        Task<IEnumerable<CompanyLogo>> GetAllLogosAsync();
        Task<bool> DeleteLogoAsync(int id);
    }
}
