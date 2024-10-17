using FiftyByte_POC.Models;
using FiftyByte_POC.Services.Concrete;
using FiftyByte_POC.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FiftyByte_POC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyLogoController(ICompanyLogoService companyLogoService) : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadLogo([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                if (file.Length > 5 * 1024 * 1024)
                    return BadRequest("File size exceeds 5 MB.");

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    return BadRequest("Invalid file format. Only JPG, JPEG, and PNG are allowed.");

                byte[] logoData;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    logoData = memoryStream.ToArray();
                }

                var companyLogo = new CompanyLogo
                {
                    LogoName = file.FileName,
                    LogoData = logoData,
                    ContentType = file.ContentType
                };

                var result = await companyLogoService.UploadLogoAsync(companyLogo);
                return Ok(new { result.Company_Logo_Id, result.LogoName, result.ContentType });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogoById(int id)
        {
            var logo = await companyLogoService.GetLogoByIdAsync(id);

            if (logo == null)
                return NotFound();

            return File(logo.LogoData, logo.ContentType, logo.LogoName);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogos()
        {
            var logos = await companyLogoService.GetAllLogosAsync();
            return Ok(logos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogo(int id)
        {
            var result = await companyLogoService.DeleteLogoAsync(id);

            if (!result)
                return NotFound("Logo not found.");

            return NoContent();
        }
    }
}
