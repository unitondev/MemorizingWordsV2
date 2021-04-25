using System.Collections.Generic;
using System.Threading.Tasks;
using MemorizingWordsV2.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace MemorizingWordsV2.Application.Interfaces.Repositories
{
    public interface IRussianWordRepository : IRepositoryAsync<RussianWord>
    {
        Task<RussianWord> FirstOrDefaultAsync(RussianWord russianWord);
        Task<List<RussianWord>> GetRelatedWordAndPartOfSpeechAsync(RussianWord russianWord);
        Task<int> GetTheLastWordIdAsync();
        Task<int> GetTheFirstWordIdAsync();
    }
}