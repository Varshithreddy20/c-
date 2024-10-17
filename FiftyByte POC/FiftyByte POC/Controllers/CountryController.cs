using FiftyByte_POC.Models;
using FiftyByte_POC.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiftyByte_POC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController(ICountryService countryService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await countryService.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var country = await countryService.GetCountryByIdAsync(id);
            if (country == null)
                return NotFound();
            return Ok(country);
        }

        //[HttpPost]
        //public async Task<IActionResult> AddCountry([FromBody] Country country)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var createdCountry = await countryService.AddCountryAsync(country);
        //    return CreatedAtAction(nameof(GetCountryById), new { id = createdCountry.CountryId }, createdCountry);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCountry(int id, [FromBody] Country country)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (id != country.CountryId)
        //        return BadRequest("Country ID mismatch");

        //    var updated = await countryService.UpdateCountryAsync(country);
        //    if (!updated)
        //        return NotFound();

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCountry(int id)
        //{
        //    var deleted = await countryService.DeleteCountryAsync(id);
        //    if (!deleted)
        //        return NotFound();

        //    return NoContent();
        //}
    }
}
