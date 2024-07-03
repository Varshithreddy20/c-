using CropDev.Models;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Models.PriceQuote;

namespace CropDev.Repository.Interface
{
    public interface IPriceQuoteRepository
    {   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createPriceQuote"></param>
        /// <returns></returns>
        Task<ResultStatus> Create(CreatePriceQuote createPriceQuote);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UpdatePriceQuote"></param>
        /// <returns></returns>
        Task<ResultStatus> Update(UpdatePriceQuote UpdatePriceQuote);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="priceQuoteId"></param>
        /// <returns></returns>
        Task<PriceQuote> GetById(int priceQuoteId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<ResultStatus> SoftDelete(int Id, string updatedBy);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<ResultStatus> Restore(int Id, string updatedBy);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<PriceQuote>> GetAll();
    }
}
