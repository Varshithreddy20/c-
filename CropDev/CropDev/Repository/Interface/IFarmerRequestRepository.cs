using CropDev.Models.FarmerRequest;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CropDev.Repository.Interface
{
    public interface IFarmerRequestRepository
    {   /// <summary>
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
        /// <param name="farmerRequestId"></param>
        /// <returns></returns>
        Task<FarmerRequest> GetById(int farmerRequestId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateFarmerRequest"></param>
        /// <returns></returns>
        Task<ResultStatus> Update(UpdateFarmerRequest updateFarmerRequest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createFarmerRequest"></param>
        /// <returns></returns>
        Task<ResultStatus> Create(CreateFarmerRequest createFarmerRequest);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<FarmerRequest>> GetAll();
    }
}
