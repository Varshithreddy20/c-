using CropDev.Models;
using CropDev.Utilities.Enums;
using CropDev.Models.Farmers;

namespace CropDev.Service.Interface
{
    public interface IFarmersService
    {
        Task<ResultStatus> SoftDelete(int Id, string updatedBy);
        Task<ResultStatus> Restore(int Id, string updatedBy);
        Task<Farmers> GetById(int farmerId);
        Task<ResultStatus> Update(UpdateFarmers updateFarmers);
        Task<ResultStatus> Create(CreateFarmers createFarmers);
        Task<List<Farmers>> GetAll();
    }
}
