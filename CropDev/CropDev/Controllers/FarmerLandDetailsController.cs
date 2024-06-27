using CropDev.Models;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CropDev.Service.Concrete;

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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var farmers = await _farmerLandDetailsService.GetAll();
            return Ok(farmers);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] FarmerLandDetails farmerLandDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest("Required Data Not Found");

            var result = await _farmerLandDetailsService.Create(farmerLandDetails);

            return result switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.DuplicateEntry => Conflict("Farmer already exists."),
                _ => BadRequest("Unable to post the data")
            };
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FarmerLandDetails farmerLandDetails)
        {
            if (!ModelState.IsValid || farmerLandDetails.FarmerLandDetailsId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not found");

            var result = await _farmerLandDetailsService.Update(farmerLandDetails);

            return result == ResultStatus.Success ? Ok(result)
                    : StatusCode(StatusCodes.Status400BadRequest, "Unable to Update Farmer");
        }
        [HttpGet("GetById/{farmerLandDetailsId}")]
        public async Task<IActionResult> GetById(int farmerLandDetailsId)
        {
            var result = await _farmerLandDetailsService.GetById(farmerLandDetailsId);

            return (result != null && result.FarmerId != 0) ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, "No Results Found.");
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> SoftDelete(int id, [FromQuery] string updatedBy)
        {
            var result = await _farmerLandDetailsService.SoftDelete(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("Farmer soft deleted successfully.");
            }
            return BadRequest("Failed to soft delete the farmer.");
        }

        [HttpPatch("restore/{id}")]
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

