using FiftyByte_POC.Models;
using FiftyByte_POC.Repositories.Concrete;
using FiftyByte_POC.Repositories.Interface;
using FiftyByte_POC.Services.Interface;

namespace FiftyByte_POC.Services.Concrete
{
    public class CompanyLogoService(ICompanyLogoRepository companyLogoRepository) : ICompanyLogoService
    {   /// <summary>
    /// to upload the logo 
    /// </summary>
    /// <param name="companyLogo"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
        public async Task<CompanyLogo> UploadLogoAsync(CompanyLogo companyLogo)
        {
            try
            {
                return await companyLogoRepository.AddLogoAsync(companyLogo);
            }
            catch (Exception ex)
            {
               
                throw new Exception($"Error uploading logo: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// get logo by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CompanyLogo> GetLogoByIdAsync(int id)
            {
                return await companyLogoRepository.GetLogoByIdAsync(id);
            }
        /// <summary>
        /// to get all the logos
        /// </summary>
        /// <returns></returns>

            public async Task<IEnumerable<CompanyLogo>> GetAllLogosAsync()
            {
                return await companyLogoRepository.GetAllLogosAsync();
            }

            public async Task<bool> DeleteLogoAsync(int id)
            {
                return await companyLogoRepository.DeleteLogoAsync(id);
            }
        }
    }

