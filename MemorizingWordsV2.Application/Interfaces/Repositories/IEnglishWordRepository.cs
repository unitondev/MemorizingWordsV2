using System.Threading.Tasks;
using MemorizingWordsV2.Domain.Models;

namespace MemorizingWordsV2.Application.Interfaces.Repositories
{
    public interface IEnglishWordRepository : IRepositoryAsync<EnglishWord>
    {
        Task<EnglishWord> FirstOrDefaultAsync(EnglishWord englishWord);
    }
}