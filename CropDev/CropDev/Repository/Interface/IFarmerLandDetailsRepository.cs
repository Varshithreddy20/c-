using CropDev.Models;
using CropDev.Utilities.Enums;
using CropDev.Models.FarmerLadDetails;

namespace CropDev.Repository.Interface
{
    public interface IFarmerLandDetailsRepository
    {
        Task<ResultStatus> SoftDelete(int farmerLandDetailsId, string updatedBy);
        Task<ResultStatus> Restore(int farmerLandDetailsId, string updatedBy);
        Task<FarmerLandDetails> GetById(int farmerLandDetailsId);
        Task<ResultStatus> Update(UpdateFarmerLandDetails updateFarmerLandDetails);
        Task<ResultStatus> Create(CreateFarmerLandDetails createFarmerLandDetails);
        Task<List<FarmerLandDetails>> GetAll();
    }
}
