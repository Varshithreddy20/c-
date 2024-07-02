using CropDev.Models;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Models.PriceQuote;

namespace CropDev.Repository.Interface
{
    public interface IPriceQuoteRepository
    {
        Task<ResultStatus> Create(CreatePriceQuote createPriceQuote);
        Task<ResultStatus> Update(UpdatePriceQuote UpdatePriceQuote);
        Task<PriceQuote> GetById(int priceQuoteId);
        Task<ResultStatus> SoftDelete(int Id, string updatedBy);
        Task<ResultStatus> Restore(int Id, string updatedBy);
        Task<List<PriceQuote>> GetAll();
    }
}
