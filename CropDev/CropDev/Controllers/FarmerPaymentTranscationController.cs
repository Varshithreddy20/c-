using CropDev.Models.FPT;
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
    public class FarmerPaymentTransactionController : ControllerBase
    {
        private readonly IFarmerPaymentTransactionService _farmerPaymentTransactionService;

        public FarmerPaymentTransactionController(IFarmerPaymentTransactionService farmerPaymentTransactionService)
        {
            _farmerPaymentTransactionService = farmerPaymentTransactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var farmers = await _farmerPaymentTransactionService.GetAll();
            return Ok(farmers);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFarmerPaymentTransaction createFarmerPaymentTransaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Required Data Not Found");

            var result = await _farmerPaymentTransactionService.Create(createFarmerPaymentTransaction);

            return result switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.DuplicateEntry => Conflict("Farmer payment transaction already exists."),
                _ => BadRequest("Unable to post the data")
            };
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFarmerPaymentTransaction updateFarmerPaymentTransaction)
        {
            if (!ModelState.IsValid || updateFarmerPaymentTransaction.FarmerPaymentTransactionId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not Found");

            var result = await _farmerPaymentTransactionService.Update(updateFarmerPaymentTransaction);

            return result == ResultStatus.Success ? Ok(result) : StatusCode(StatusCodes.Status400BadRequest, "Unable to Update Farmer Payment Transaction");
        }

        [HttpGet("GetById/{farmerPaymentTransactionId}")]
        public async Task<IActionResult> GetById(int farmerPaymentTransactionId)
        {
            var result = await _farmerPaymentTransactionService.GetById(farmerPaymentTransactionId);

            return (result != null && result.FarmerPaymentTransactionId != null) ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, "No Results Found.");
        }

        [HttpDelete]
        public async Task<IActionResult> SoftDelete(int id, [FromQuery] string updatedBy)
        {
            var result = await _farmerPaymentTransactionService.SoftDelete(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("Farmer payment transaction soft deleted successfully.");
            }
            return BadRequest("Failed to soft delete the farmer payment transaction.");
        }

        [HttpPatch]
        public async Task<IActionResult> Restore(int id, [FromQuery] string updatedBy)
        {
            var result = await _farmerPaymentTransactionService.Restore(id, updatedBy);
            if (result == ResultStatus.Success)
            {
                return Ok("Farmer payment transaction restored successfully.");
            }
            return BadRequest("Failed to restore the farmer payment transaction.");
        }
    }
}
