using CropDev.Models;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Models.Farmers;

namespace CropDev.Repository.Interface
{
    public interface IFarmersRepository
    {   /// <summary>
    /// 
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="updatedBy"></param>
    /// <returns></returns>
        Task<ResultStatus> SoftDelete(int Id, string updatedBy);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<ResultStatus> Restore(int Id, string updatedBy);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmerId"></param>
        /// <returns></returns>
        Task<Farmers> GetById(int farmerId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateFarmers"></param>
        /// <returns></returns>
        Task<ResultStatus> Update(UpdateFarmers updateFarmers);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createFarmers"></param>
        /// <returns></returns>
        Task<ResultStatus> Create(CreateFarmers createFarmers);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<Farmers>> GetAll();
    }
}
