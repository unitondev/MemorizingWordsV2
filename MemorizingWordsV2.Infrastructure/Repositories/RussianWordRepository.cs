using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemorizingWordsV2.Application.Interfaces.Repositories;
using MemorizingWordsV2.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace MemorizingWordsV2.Infrastructure.Repositories
{
    public class RussianWordRepository : RepositoryAsync<RussianWord>, IRussianWordRepository
    {
        private WordsDBContext _dbContext;
        public RussianWordRepository(WordsDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RussianWord> FirstOrDefaultAsync(RussianWord russianWord)
        {
            return await _dbContext.RussianWords.FirstOrDefaultAsync(word => word.Word == russianWord.Word);
        }
        
        //TODO return just necessary field, not 4 objects 
        public async Task<List<RussianWord>> GetRelatedWordAndPartOfSpeechAsync(RussianWord russianWord)
        {
            var relatedWordAndPartOfSpeech = await _dbContext.RussianWords
                .Where(rusnWord => rusnWord.Id == russianWord.Id)
                .Include(rusWord => rusWord.EnglishRussianWords)
                .ThenInclude(engRusWord => engRusWord.English)
                .ThenInclude(engWord => engWord.PartOfSpeech)
                .ToListAsync();
            
            return relatedWordAndPartOfSpeech;
        }
    }
}