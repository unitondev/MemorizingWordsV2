using System.Collections.Generic;
using System.Threading.Tasks;
using MemorizingWordsV2.Domain.Models;

namespace MemorizingWordsV2.Application.Interfaces.Services
{
    public interface IEnglishWordService
    {
        public Task<IEnumerable<EnglishWord>> GetAllEnglishWordsAsync();
        public Task<EnglishWord> GetByIdOrDefaultAsync(int id);
    }
}