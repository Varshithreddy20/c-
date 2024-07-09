using CropDev.Models.FarmerRequest;
using CropDev.Models.FarmerLadDetails;
using CropDev.Service.Concrete;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace CropDev.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FarmerRequestController : ControllerBase
    {
        private readonly IFarmerRequestService _farmersRequestService;

        public FarmerRequestController(IFarmerRequestService farmersRequestService)
        {
            _farmersRequestService = farmersRequestService;
        }
/// <summary>
/// Gets all Farmer Requests
/// </summary>
/// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var farmers = await _farmersRequestService.GetAll();
            return Ok(farmers);
        }
        /// <summary>
        /// Creates a new Farmer Request
        /// </summary>
        /// <param name="createFarmerRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFarmerRequest createFarmerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _farmersRequestService.Create(createFarmerRequest);

            return result switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.DuplicateEntry => Conflict("Farmer already exists."),
                _ => BadRequest("Unable to post the data")
            };
        }

        /// <summary>
        /// Updates a Existing Farmer Request
        /// </summary>
        /// <param name="UpdateFarmerRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFarmerRequest UpdateFarmerRequest)
        {
            if (!ModelState.IsValid || UpdateFarmerRequest.FarmerRequestId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not found");

            var result = await _farmersRequestService.Update(UpdateFarmerRequest);

            return result == ResultStatus.Success ? Ok(result)
                    : StatusCode(StatusCodes.Status400BadRequest, "Unable to Update FarmerRequest");
        }
        /// <summary>
        /// Gets a Farmer Request by Id
        /// </summary>
        /// <param name="farmerRequestId"></param>
        /// <returns></returns>
        [HttpGet("GetById/{farmerRequestId}")]
        public async Task<IActionResult> GetById(int farmerRequestId)
        {
            var result = await _farmersRequestService.GetById(farmerRequestId);

            return (result != null && result.FarmerRequestId != 0) ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, "No Results Found.");
        }
        /// <summary>
        /// Deletes a Farmer Request by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> SoftDelete(int id, [FromQuery] string updatedBy)
        {
            var result = await _farmersRequestService.SoftDelete(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("Farmer Request is soft deleted successfully.");
            }
            return BadRequest("Failed to soft delete the Farmer Request.");
        }
        /// <summary>
        /// Restores a deleted Farmer Request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Restore(int id, [FromQuery] string updatedBy)
        {
            var result = await _farmersRequestService.Restore(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("Farmer Request restored successfully.");
            }
            return BadRequest("Failed to restore the Farmer Request.");
        }
    }
}
