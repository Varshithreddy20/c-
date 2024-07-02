using CropDev.Models.FPT;
using CropDev.Repository.Concrete;
using CropDev.Repository.Interface;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CropDev.Service.Concrete
{
    namespace CropDev.Service.Concrete
    {
        public class FarmerPaymentTransactionService : IFarmerPaymentTransactionService
        {
            private readonly IFarmerPaymentTransactionRepository _farmerPaymentTransactionRepository;

            public FarmerPaymentTransactionService(IFarmerPaymentTransactionRepository farmerPaymentTransactionRepository)
            {
                _farmerPaymentTransactionRepository = farmerPaymentTransactionRepository;
            }

            public async Task<List<FarmerPaymentTransaction>> GetAll()
            {
                return await _farmerPaymentTransactionRepository.GetAll();
            }

            public async Task<ResultStatus> Create(CreateFarmerPaymentTransaction createFarmerPaymentTransaction)
            {
                return await _farmerPaymentTransactionRepository.Create(createFarmerPaymentTransaction);
            }

            public async Task<ResultStatus> Update(UpdateFarmerPaymentTransaction updateFarmerPaymentTransaction)
            {
                return await _farmerPaymentTransactionRepository.Update(updateFarmerPaymentTransaction);
            }

            public async Task<FarmerPaymentTransaction> GetById(int farmerPaymentTransactionId)
            {
                return await _farmerPaymentTransactionRepository.GetById(farmerPaymentTransactionId);
            }

            public async Task<ResultStatus> SoftDelete(int id, string updatedBy)
            {
                return await _farmerPaymentTransactionRepository.SoftDelete(id, updatedBy);
            }

            public async Task<ResultStatus> Restore(int id, string updatedBy)
            {
                return await _farmerPaymentTransactionRepository.Restore(id, updatedBy);
            }
        }
    }
}
