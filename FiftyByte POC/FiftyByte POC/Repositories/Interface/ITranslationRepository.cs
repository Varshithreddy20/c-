using System.Threading.Tasks;

namespace FiftyByte_POC.Repositories.Interfaces
{
    public interface ITranslationRepository
    {
        Task<string> GetArabicTextAsync(string englishText);
    }
}
