using MemorizingWordsV2.Application.Interfaces.Repositories;
using MemorizingWordsV2.Domain.Models;

namespace MemorizingWordsV2.Infrastructure.Repositories
{
    public class EnglishWordRepository : RepositoryAsync<EnglishWord>, IEnglishWordRepository
    {
        public EnglishWordRepository(WordsDBContext dbContext) : base(dbContext)
        {
        }
    }
}