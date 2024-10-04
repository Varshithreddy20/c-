using CRAVENEST.Model;
using CRAVENEST.Utilities.Eums;

namespace CRAVENEST.Repository.Interface
{
    public interface IFoodItemRepository
    {
        Task<ResultStatus> Create(FoodItem foodItem);
        Task<ResultStatus> Delete(int id);
        Task<List<FoodItem>> GetAll();
        Task<List<FoodItem>> GetByCuisine(string cuisine);
        Task<ResultStatus> UpdateAvailability(int id, bool available);
    }
}