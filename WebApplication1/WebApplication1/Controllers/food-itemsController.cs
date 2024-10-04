using CRAVENEST.Model;
using CRAVENEST.Service.Interface;
using CRAVENEST.Utilities.Eums;
using Microsoft.AspNetCore.Mvc;

namespace CRAVENEST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodItemsController(IFoodItemService foodItemService, ILogger<FoodItemsController> logger) : ControllerBase
    {  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="foodItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateFoodItem([FromBody] FoodItem foodItem)
        {
            var result = await foodItemService.CreateFoodItem(foodItem);
            return result == ResultStatus.Success ? Ok() : BadRequest();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodItem(int id)
        {
            var result = await foodItemService.DeleteFoodItem(id);
            return result == ResultStatus.Success ? Ok() : NotFound();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllFoodItems()
        {
            var foodItems = await foodItemService.GetAllFoodItems();
            return Ok(foodItems);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuisine"></param>
        /// <returns></returns>
        [HttpGet("cuisine/{cuisine}")]
        public async Task<IActionResult> GetFoodItemsByCuisine(string cuisine)
        {
            var foodItems = await foodItemService.GetFoodItemsByCuisine(cuisine);
            return Ok(foodItems);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="available"></param>
        /// <returns></returns>
        [HttpPatch("{id}/availability")]
        public async Task<IActionResult> UpdateFoodItemAvailability(int id, [FromBody] bool available)
        {
            var result = await foodItemService.UpdateFoodItemAvailability(id, available);
            return result == ResultStatus.Success ? Ok() : NotFound();
        }
    }
}