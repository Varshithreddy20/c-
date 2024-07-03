using CropDev.Models;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CropDev.Service.Concrete;
using CropDev.Models.FarmerLadDetails;

namespace CropDev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmerLandDetailsController : ControllerBase
    {
        private readonly IFarmerLandDetailsService _farmerLandDetailsService;

        public FarmerLandDetailsController(IFarmerLandDetailsService farmerLandDetailsService)
        {
            _farmerLandDetailsService = farmerLandDetailsService;
        }
        /// <summary>
        /// Gets all Farmer Land Details inputs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var farmers = await _farmerLandDetailsService.GetAll();
            return Ok(farmers);
        }
        /// <summary>
        /// Creates Farmer Land Details 
        /// </summary>
        /// <param name="CreateFarmerLandDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFarmerLandDetails CreateFarmerLandDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest("Required Data Not Found");

            var result = await _farmerLandDetailsService.Create(CreateFarmerLandDetails);

            return result switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.DuplicateEntry => Conflict("Farmer already exists."),
                _ => BadRequest("Unable to post the data")
            };
        }
        /// <summary>
        /// Updates Farmer Land Details 
        /// </summary>
        /// <param name="updateFarmerLandDetails"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFarmerLandDetails updateFarmerLandDetails)
        {
            if (!ModelState.IsValid || updateFarmerLandDetails.FarmerLandDetailsId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not found");

            var result = await _farmerLandDetailsService.Update(updateFarmerLandDetails);

            return result == ResultStatus.Success ? Ok(result)
                    : StatusCode(StatusCodes.Status400BadRequest, "Unable to Update Farmer");
        }
        /// <summary>
        /// Gets Farmer Land Details inputs by Id
        /// </summary>
        /// <param name="farmerLandDetailsId"></param>
        /// <returns></returns>
        [HttpGet("GetById/{farmerLandDetailsId}")]
        public async Task<IActionResult> GetById(int farmerLandDetailsId)
        {
            var result = await _farmerLandDetailsService.GetById(farmerLandDetailsId);

            return (result != null && result.FarmerId != 0) ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, "No Results Found.");
        }
        /// <summary>
        /// Deletes Farmer Land Details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> SoftDelete(int id, [FromQuery] string updatedBy)
        {
            var result = await _farmerLandDetailsService.SoftDelete(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("Farmer soft deleted successfully.");
            }
            return BadRequest("Failed to soft delete the farmer.");
        }
        /// <summary>
        /// Restores Farmer Land Details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>

        [HttpPatch]
        public async Task<IActionResult> Restore(int id, [FromQuery] string updatedBy)
        {
            var result = await _farmerLandDetailsService.Restore(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("Farmer restored successfully.");
            }
            return BadRequest("Failed to restore the farmer.");
        }
    }

}

