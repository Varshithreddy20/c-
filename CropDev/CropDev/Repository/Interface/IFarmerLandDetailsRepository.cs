using CropDev.Models;
using CropDev.Utilities.Enums;
using CropDev.Models.FarmerLadDetails;

namespace CropDev.Repository.Interface
{
    public interface IFarmerLandDetailsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmerLandDetailsId"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<ResultStatus> SoftDelete(int farmerLandDetailsId, string updatedBy);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmerLandDetailsId"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<ResultStatus> Restore(int farmerLandDetailsId, string updatedBy);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmerLandDetailsId"></param>
        /// <returns></returns>
        Task<FarmerLandDetails> GetById(int farmerLandDetailsId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateFarmerLandDetails"></param>
        /// <returns></returns>
        Task<ResultStatus> Update(UpdateFarmerLandDetails updateFarmerLandDetails);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createFarmerLandDetails"></param>
        /// <returns></returns>
        Task<ResultStatus> Create(CreateFarmerLandDetails createFarmerLandDetails);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<FarmerLandDetails>> GetAll();
    }
}
