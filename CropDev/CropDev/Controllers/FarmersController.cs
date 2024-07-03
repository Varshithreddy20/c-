using CropDev.Models;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Models.Farmers;

namespace CropDev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmersController : ControllerBase
    {
        private readonly IFarmersService _farmersService;

        public FarmersController(IFarmersService farmersService)
        {
            _farmersService = farmersService;
        }
        /// <summary>
        /// Gets all Farmer Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var farmers = await _farmersService.GetAll();
            return Ok(farmers);
        }
        /// <summary>
        /// Create a new Farmer
        /// </summary>
        /// <param name="createFarmers"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFarmers createFarmers)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not found");

            var result = await _farmersService.Create(createFarmers);

            return (result == ResultStatus.Success) ? Ok(result)
                    : (result == ResultStatus.DuplicateEntry) ? StatusCode(StatusCodes.Status409Conflict, "Farmer already exists.")
                    : StatusCode(StatusCodes.Status400BadRequest, "Unable to post the data");
        }
        /// <summary>
        /// Update a existing Farmer
        /// </summary>
        /// <param name="updateFarmers"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFarmers updateFarmers)
        {
            if (!ModelState.IsValid || updateFarmers.FarmerId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not found");

            var result = await _farmersService.Update(updateFarmers);

            return result == ResultStatus.Success ? Ok(result)
                    : StatusCode(StatusCodes.Status400BadRequest, "Unable to Update Farmer");
        }
        /// <summary>
        /// Gets a Farmer Details by Id
        /// </summary>
        /// <param name="farmerId"></param>
        /// <returns></returns>
        [HttpGet("GetFarmer/{farmerId}")]
        public async Task<IActionResult> GetById(int farmerId)
        {
            var result = await _farmersService.GetById(farmerId);

            return (result != null && result.FarmerId != 0) ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, "No Results Found.");
        }
        /// <summary>
        /// Deletes a existing Farmer Details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> SoftDelete(int id, [FromQuery] string updatedBy)
        {
            var result = await _farmersService.SoftDelete(id,updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("Farmer soft deleted successfully.");
            }
            return BadRequest("Failed to soft delete the farmer.");
        }
        /// <summary>
        /// Restores a deleted farmer details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Restore(int id, [FromQuery] string updatedBy)
        {
            var result = await _farmersService.Restore(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("Farmer restored successfully.");
            }
            return BadRequest("Failed to restore the farmer.");
        }
    }
}
