using CropDev.Models;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Models.Farmers;

namespace CropDev.Repository.Interface
{
    public interface IFarmersRepository
    {
        Task<ResultStatus> SoftDelete(int Id, string updatedBy);
        Task<ResultStatus> Restore(int Id, string updatedBy);
        Task<Farmers> GetById(int farmerId);
        Task<ResultStatus> Update(UpdateFarmers updateFarmers);
        Task<ResultStatus> Create(CreateFarmers createFarmers);
        Task<List<Farmers>> GetAll();
    }
}
