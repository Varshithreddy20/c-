using CRAVENEST.Model;
using CRAVENEST.Utilities.Eums;

namespace CRAVENEST.Service.Interface
{
    public interface IFoodItemService
    {
        Task<ResultStatus> CreateFoodItem(FoodItem foodItem);
        Task<ResultStatus> DeleteFoodItem(int id);
        Task<List<FoodItem>> GetAllFoodItems();
        Task<List<FoodItem>> GetFoodItemsByCuisine(string cuisine);
        Task<ResultStatus> UpdateFoodItemAvailability(int id, bool available);
    }
}