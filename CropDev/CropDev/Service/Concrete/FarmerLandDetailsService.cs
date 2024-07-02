using CropDev.Repository.Interface;
using CropDev.Service.Interface;
using CropDev.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Repository.Concrete;
using CropDev.Utilities.Enums;
using CropDev.Models.FarmerLadDetails;

namespace CropDev.Service.Concrete
{
    public class FarmerLandDetailsService : IFarmerLandDetailsService
    {
        private readonly IFarmerLandDetailsRepository _farmerLandDetailsRepository;

        public FarmerLandDetailsService(IFarmerLandDetailsRepository farmerLandDetailsRepository)
        {
            _farmerLandDetailsRepository = farmerLandDetailsRepository;
        }

        public async Task<List<FarmerLandDetails>> GetAll()
        {
            return await _farmerLandDetailsRepository.GetAll();
        }
        public async Task<ResultStatus> Create(CreateFarmerLandDetails createFarmerLandDetails)
        {
            return await _farmerLandDetailsRepository.Create(createFarmerLandDetails);
        }
        public async Task<ResultStatus> Update(UpdateFarmerLandDetails updateFarmerLandDetails)
        {
            return await _farmerLandDetailsRepository.Update(updateFarmerLandDetails);
        }
        public async Task<FarmerLandDetails> GetById(int farmerLandDetailsId)
        {
            return await _farmerLandDetailsRepository.GetById(farmerLandDetailsId);
        }
        public async Task<ResultStatus> SoftDelete(int farmerLandDetailsId, string updatedBy)
        {
            return await _farmerLandDetailsRepository.SoftDelete(farmerLandDetailsId, updatedBy);
        }

        public async Task<ResultStatus> Restore(int farmerLandDetailsId, string updatedBy)
        {
            return await _farmerLandDetailsRepository.Restore(farmerLandDetailsId, updatedBy);
        }
    }
}
