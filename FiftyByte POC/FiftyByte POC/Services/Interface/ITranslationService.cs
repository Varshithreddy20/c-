using System.Threading.Tasks;

namespace FiftyByte_POC.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<string> TranslateToArabicAsync(string englishText);
    }
}
