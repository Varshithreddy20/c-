using CropDev.Models;
using CropDev.Repository.Interface;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Models.Farmers;

namespace CropDev.Service.Concrete
{
    public class FarmersService : IFarmersService
    {
        private readonly IFarmersRepository _farmerRepository;

        public FarmersService(IFarmersRepository farmerRepository)
        {
            _farmerRepository = farmerRepository;
        }

        public async Task<List<Farmers>> GetAll()
        {
            return await _farmerRepository.GetAll();
        }

        public async Task<ResultStatus> Create(CreateFarmers createFarmers)
        {
            return await _farmerRepository.Create(createFarmers);
        }

        public async Task<ResultStatus> Update(UpdateFarmers updateFarmers)
        {
            return await _farmerRepository.Update(updateFarmers);
        }

        public async Task<Farmers> GetById(int farmerId)
        {
            return await _farmerRepository.GetById(farmerId);
        }

        public async Task<ResultStatus> SoftDelete(int farmerId, string updatedBy)
        {
            return await _farmerRepository.SoftDelete(farmerId, updatedBy);
        }

        public async Task<ResultStatus> Restore(int farmerId, string updatedBy)
        {
            return await _farmerRepository.Restore(farmerId, updatedBy);
        }
    }
}
