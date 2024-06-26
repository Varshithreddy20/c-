using CropDev.Models;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet("GetAllFarmers")]
        public async Task<IActionResult> GetAll()
        {
            var farmers = await _farmersService.GetAll();
            return Ok(farmers);
        }

        [HttpPost("CreateFarmer")]
        public async Task<IActionResult> Create([FromBody] Farmers farmers)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not found");

            var result = await _farmersService.Create(farmers);

            return (result == ResultStatus.Success) ? Ok(result)
                    : (result == ResultStatus.DuplicateEntry) ? StatusCode(StatusCodes.Status409Conflict, "Farmer already exists.")
                    : StatusCode(StatusCodes.Status400BadRequest, "Unable to post the data");
        }

        [HttpPut("UpdateFarmer")]
        public async Task<IActionResult> Update([FromBody] Farmers farmers)
        {
            if (!ModelState.IsValid || farmers.FarmerId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not found");

            var result = await _farmersService.Update(farmers);

            return result == ResultStatus.Success ? Ok(result)
                    : StatusCode(StatusCodes.Status400BadRequest, "Unable to Update Farmer");
        }

        [HttpGet("GetFarmer/{farmerId}")]
        public async Task<IActionResult> GetById(int farmerId)
        {
            var result = await _farmersService.GetById(farmerId);

            return (result != null && result.FarmerId != 0) ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, "No Results Found.");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> SoftDelete(int id, [FromQuery] string updatedBy)
        {
            var result = await _farmersService.SoftDelete(id,updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("Farmer soft deleted successfully.");
            }
            return BadRequest("Failed to soft delete the farmer.");
        }

        [HttpPatch("restore/{id}")]
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
