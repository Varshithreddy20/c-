using CRAVENEST.Model;
using CRAVENEST.Repository.Interface;
using CRAVENEST.Service.Interface;
using CRAVENEST.Utilities.Eums;
using Microsoft.Extensions.Logging;

namespace CRAVENEST.Service.Concrete
{
    public class FoodItemService : IFoodItemService
    {
        private readonly IFoodItemRepository _repository;
        private readonly ILogger<FoodItemService> _logger;

        public FoodItemService(IFoodItemRepository repository, ILogger<FoodItemService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ResultStatus> CreateFoodItem(FoodItem foodItem)
        {
            try
            {
                return await _repository.Create(foodItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating food item");
                return ResultStatus.Failed;
            }
        }

        public async Task<ResultStatus> DeleteFoodItem(int id)
        {
            try
            {
                return await _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting food item with id {id}");
                return ResultStatus.Failed;
            }
        }

        public async Task<List<FoodItem>> GetAllFoodItems()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all food items");
                return new List<FoodItem>();
            }
        }

        public async Task<List<FoodItem>> GetFoodItemsByCuisine(string cuisine)
        {
            try
            {
                return await _repository.GetByCuisine(cuisine);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting food items for cuisine {cuisine}");
                return new List<FoodItem>();
            }
        }

        public async Task<ResultStatus> UpdateFoodItemAvailability(int id, bool available)
        {
            try
            {
                return await _repository.UpdateAvailability(id, available);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating availability for food item with id {id}");
                return ResultStatus.Failed;
            }
        }
    }
}