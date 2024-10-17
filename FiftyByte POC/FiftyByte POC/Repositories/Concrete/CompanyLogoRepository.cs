using FiftyByte_POC.Data;
using FiftyByte_POC.Models;
using FiftyByte_POC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;


namespace FiftyByte_POC.Repositories.Concrete
{
   
        public class CompanyLogoRepository(ApplicationDbContext dbContext) : ICompanyLogoRepository
        {
        /// <summary>
        /// to add to logo
        /// </summary>
        /// <param name="companyLogo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CompanyLogo> AddLogoAsync(CompanyLogo companyLogo)
        {
            try
            {
                dbContext.CompanyLogos.Add(companyLogo);
                await dbContext.SaveChangesAsync();
                return companyLogo;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error adding logo to the database: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// get logo by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CompanyLogo> GetLogoByIdAsync(int id)
            {
                return await dbContext.CompanyLogos.FindAsync(id);
            }

            public async Task<IEnumerable<CompanyLogo>> GetAllLogosAsync()
            {
                return await dbContext.CompanyLogos.ToListAsync();
            }

            public async Task<bool> DeleteLogoAsync(int id)
            {
                var logo = await GetLogoByIdAsync(id);
                if (logo != null)
                {
                    dbContext.CompanyLogos.Remove(logo);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }
}
