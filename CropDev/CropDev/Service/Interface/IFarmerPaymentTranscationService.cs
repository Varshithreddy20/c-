using CropDev.Models.FPT;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CropDev.Service.Interface
{
    public interface IFarmerPaymentTransactionService
    {
        Task<ResultStatus> SoftDelete(int id, string updatedBy);
        Task<ResultStatus> Restore(int id, string updatedBy);
        Task<FarmerPaymentTransaction> GetById(int farmerPaymentTransactionId);
        Task<ResultStatus> Update(UpdateFarmerPaymentTransaction updateFarmerPaymentTransaction);
        Task<ResultStatus> Create(CreateFarmerPaymentTransaction createFarmerPaymentTransaction);
        Task<List<FarmerPaymentTransaction>> GetAll();
    }
}