using FiftyByte_POC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiftyByte_POC.Services.Interface
{
    public interface ICompanyLogoService
    {
        Task<CompanyLogo> UploadLogoAsync(CompanyLogo companyLogo);
        Task<CompanyLogo> GetLogoByIdAsync(int id);
        Task<IEnumerable<CompanyLogo>> GetAllLogosAsync();
        Task<bool> DeleteLogoAsync(int id);
    }
}
