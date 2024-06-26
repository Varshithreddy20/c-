using CropDev.Models;
using CropDev.Utilities.Enums;

namespace CropDev.Service.Interface
{
    public interface IFarmersService
    {
        Task<ResultStatus> SoftDelete(int farmerId, string updatedBy);
        Task<ResultStatus> Restore(int farmerId, string updatedBy);
        Task<Farmers> GetById(int farmerId);
        Task<ResultStatus> Update(Farmers farmers);
        Task<ResultStatus> Create(Farmers farmers);
        Task<List<Farmers>> GetAll();
    }
}
