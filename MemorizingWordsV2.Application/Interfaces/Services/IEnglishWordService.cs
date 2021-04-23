using System.Collections.Generic;
using System.Threading.Tasks;
using MemorizingWordsV2.Domain.Models;

namespace MemorizingWordsV2.Application.Interfaces.Services
{
    public interface IEnglishWordService
    {
        public Task<IEnumerable<EnglishWord>> GetAllEnglishWordsAsync();
        public Task<EnglishWord> GetByIdOrDefaultAsync(int id);
        Task<bool> AddAsync(EnglishWord item);
        Task<int> AddRangeAsync(IEnumerable<EnglishWord> items);
        Task<bool> DeleteByIdAsync(int id);
    }
}