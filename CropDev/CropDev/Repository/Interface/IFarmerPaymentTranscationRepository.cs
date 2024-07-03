using CropDev.Models.FPT;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CropDev.Repository.Interface
{
    public interface IFarmerPaymentTransactionRepository
    {   /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedBy"></param>
    /// <returns></returns>
        Task<ResultStatus> SoftDelete(int id, string updatedBy);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<ResultStatus> Restore(int id, string updatedBy);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmerPaymentTransactionId"></param>
        /// <returns></returns>
        Task<FarmerPaymentTransaction> GetById(int farmerPaymentTransactionId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateFarmerPaymentTransaction"></param>
        /// <returns></returns>
        Task<ResultStatus> Update(UpdateFarmerPaymentTransaction updateFarmerPaymentTransaction);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createFarmerPaymentTransaction"></param>
        /// <returns></returns>
        Task<ResultStatus> Create(CreateFarmerPaymentTransaction createFarmerPaymentTransaction);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<FarmerPaymentTransaction>> GetAll();
    }
}
