using CropDev.Models;
using CropDev.Repository.Interface;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Models.PriceQuote;

namespace CropDev.Service.Concrete
{
    public class PriceQuoteService : IPriceQuoteService
    {
        private readonly IPriceQuoteRepository _priceQuoteRepository;

        public PriceQuoteService(IPriceQuoteRepository priceQuoteRepository)
        {
            _priceQuoteRepository = priceQuoteRepository;
        }
        public async Task<ResultStatus> Update(UpdatePriceQuote updatePriceQuote)
        {
            return await _priceQuoteRepository.Update(updatePriceQuote);
        }
        public async Task<PriceQuote> GetById(int priceQuoteId)
        {
            return await _priceQuoteRepository.GetById(priceQuoteId);
        }
        public async Task<List<PriceQuote>> GetAll()
        {
            return await _priceQuoteRepository.GetAll();
        }
        public async Task<ResultStatus> Create(CreatePriceQuote createPriceQuote)
        {
            return await _priceQuoteRepository.Create(createPriceQuote);
        }
        public async Task<ResultStatus> SoftDelete(int priceQuoteId, string updatedBy)
        {
            return await _priceQuoteRepository.SoftDelete(priceQuoteId, updatedBy);
        }
        public async Task<ResultStatus> Restore(int priceQuoteId, string updatedBy)
        {
            return await _priceQuoteRepository.Restore(priceQuoteId, updatedBy);
        }




    }
}
