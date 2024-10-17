using FiftyByte_POC.Repositories.Interfaces;
using System.Threading.Tasks;
using FiftyByte_POC.Services.Interfaces;

namespace FiftyByte_POC.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly ITranslationRepository _translationRepository;

        public TranslationService(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        public async Task<string> TranslateToArabicAsync(string englishText)
        {
            return await _translationRepository.GetArabicTextAsync(englishText);
        }
    }
}
