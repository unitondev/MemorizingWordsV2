using System.Threading.Tasks;
using MemorizingWordsV2.Application.Interfaces.Repositories;
using MemorizingWordsV2.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MemorizingWordsV2.Infrastructure.Repositories
{
    public class EnglishWordRepository : RepositoryAsync<EnglishWord>, IEnglishWordRepository
    {
        private WordsDBContext _dbContext;
        
        public EnglishWordRepository(WordsDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<EnglishWord> FirstOrDefaultAsync(EnglishWord englishWord)
        {
            return await _dbContext.EnglishWords.FirstOrDefaultAsync(word => word.Word == englishWord.Word);
        }
    }
}