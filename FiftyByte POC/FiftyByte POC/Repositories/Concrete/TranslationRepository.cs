using FiftyByte_POC.Data;
using FiftyByte_POC.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FiftyByte_POC.Repositories.Interfaces;

namespace FiftyByte_POC.Repositories
{
    public class TranslationRepository : ITranslationRepository
    {
        private readonly ApplicationDbContext _context;

        public TranslationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetArabicTextAsync(string englishText)
        {
            var translation = await _context.Translations
                .FirstOrDefaultAsync(t => t.EnglishText == englishText);
            return translation?.ArabicText ?? englishText;
        }
    }
}
