﻿using CropDev.Models.FarmerRequest;
using CropDev.Models.FarmerLadDetails;
using CropDev.Service.Concrete;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;


namespace CropDev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmerRequestController : ControllerBase
    {
        private readonly IFarmerRequestService _farmersRequestService;

        public FarmerRequestController(IFarmerRequestService farmersRequestService)
        {
            _farmersRequestService = farmersRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var farmers = await _farmersRequestService.GetAll();
            return Ok(farmers);
        }

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
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFarmerRequest UpdateFarmerRequest)
        {
            if (!ModelState.IsValid || UpdateFarmerRequest.FarmerRequestId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not found");

            var result = await _farmersRequestService.Update(UpdateFarmerRequest);

            return result == ResultStatus.Success ? Ok(result)
                    : StatusCode(StatusCodes.Status400BadRequest, "Unable to Update FarmerRequest");
        }
        [HttpGet("GetById/{farmerRequestId}")]
        public async Task<IActionResult> GetById(int farmerRequestId)
        {
            var result = await _farmersRequestService.GetById(farmerRequestId);

            return (result != null && result.FarmerRequestId != 0) ? Ok(result) : StatusCode(StatusCodes.Status204NoContent, "No Results Found.");
        }
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