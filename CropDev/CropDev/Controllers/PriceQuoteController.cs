using CropDev.Models;
using CropDev.Service.Interface;
using CropDev.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Models.PriceQuote;

namespace CropDev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceQuoteController : ControllerBase
    {
        private readonly IPriceQuoteService _priceQuoteService;

        public PriceQuoteController(IPriceQuoteService priceQuoteService)
        {
            _priceQuoteService = priceQuoteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var priceQuotes = await _priceQuoteService.GetAll();
            return Ok(priceQuotes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePriceQuote createPriceQuote)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not Found");

            var result = await _priceQuoteService.Create(createPriceQuote);

            return result switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.DuplicateEntry => StatusCode(StatusCodes.Status409Conflict, "Already exists."),
                _ => StatusCode(StatusCodes.Status400BadRequest, "Unable to post the data")
            };
        }
        [HttpGet("GetPriceQuoteById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var priceQuote = await _priceQuoteService.GetById(id);
            if (priceQuote == null)
            {
                return NotFound();
            }
            return Ok(priceQuote);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePriceQuote updatePriceQuote)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Required Data Not Found");

            var result = await _priceQuoteService.Update(updatePriceQuote);

            return result switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.Failed => StatusCode(StatusCodes.Status400BadRequest, "Unable to update the data"),
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error")
            };
        }
        [HttpDelete]
        public async Task<IActionResult> SoftDelete(int id, [FromBody] string updatedBy)
        {
            var result = await _priceQuoteService.SoftDelete(id, updatedBy);

            return result switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.Failed => StatusCode(StatusCodes.Status400BadRequest, "Unable to soft delete the data"),
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error")
            };
        }

        [HttpPatch]
        public async Task<IActionResult> Restore(int id, [FromBody] string updatedBy)
        {
            var result = await _priceQuoteService.Restore(id, updatedBy);

            return result switch
            {
                ResultStatus.Success => Ok(result),
                ResultStatus.Failed => StatusCode(StatusCodes.Status400BadRequest, "Unable to restore the data"),
                _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error")
            };
        }


    }
}
