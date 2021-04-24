using System.Collections.Generic;
using System.Threading.Tasks;
using MemorizingWordsV2.Domain.Models;

namespace MemorizingWordsV2.Application.Interfaces.Services
{
    public interface IRussianWordService
    {
        Task<List<RussianWord>> GetRelatedPairByIdAsync(int id);
        Task<bool> AddPairAsync(RussianWord russianWord, EnglishWord englishWord);
        Task<int> AddPairRangeAsync(IEnumerable<RussianWord> russianWords, IEnumerable<EnglishWord> englishWords);
        Task<bool> DeletePairByIdAsync(int id);
    }
}