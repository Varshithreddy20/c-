using CropDev.Models;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CropDev.Repository.Interface
{
    public interface IFarmersRepository
    {
        Task<ResultStatus> SoftDelete(int farmerId, string updatedBy);
        Task<ResultStatus> Restore(int farmerId, string updatedBy);
        Task<Farmers> GetById(int farmerId);
        Task<ResultStatus> Update(Farmers farmers);
        Task<ResultStatus> Create(Farmers farmers);
        Task<List<Farmers>> GetAll();
    }
}
