using FiftyByte_POC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FiftyByte_POC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService _translationService;

        public TranslationController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [HttpGet("translate")]
        public async Task<IActionResult> Translate([FromQuery] string text)
        {
            var translatedText = await _translationService.TranslateToArabicAsync(text);
            return Ok(new { ArabicText = translatedText });
        }
    }
}
