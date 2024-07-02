using CropDev.Models.FarmerRequest;
using CropDev.Repository.Concrete;
using CropDev.Repository.Interface;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CropDev.Service.Concrete
{
    public class FarmerRequestService : IFarmerRequestService
    {
        private readonly IFarmerRequestRepository _farmerRequestRepository;

        public FarmerRequestService(IFarmerRequestRepository farmerRequestRepository)
        {
            _farmerRequestRepository = farmerRequestRepository;
        }

        public Task<ResultStatus> Create(CreateFarmerRequest createFarmerRequest)
        {
            return _farmerRequestRepository.Create(createFarmerRequest);
        }

        public Task<List<FarmerRequest>> GetAll()
        {
            return _farmerRequestRepository.GetAll();
        }

        public Task<FarmerRequest> GetById(int farmerRequestId)
        {
            return _farmerRequestRepository.GetById(farmerRequestId);
        }

        public Task<ResultStatus> Update(UpdateFarmerRequest updateFarmerRequest)
        {
            return _farmerRequestRepository.Update(updateFarmerRequest);
        }
        public async Task<ResultStatus> SoftDelete(int farmerRequestId, string updatedBy)
        {
            return await _farmerRequestRepository.SoftDelete(farmerRequestId, updatedBy);
        }

        public async Task<ResultStatus> Restore(int farmerRequestId, string updatedBy)
        {
            return await _farmerRequestRepository.Restore(farmerRequestId, updatedBy);
        }
    }
}
