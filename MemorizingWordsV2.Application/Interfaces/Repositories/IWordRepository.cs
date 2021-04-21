using System.Collections.Generic;
using MemorizingWordsV2.Domain.Models;

namespace MemorizingWordsV2.Application.Interfaces.Repositories
{
    public interface IWordRepository
    {
        EnglishWord GetWordWordById(int id);
        List<EnglishWord> GetAllWords();
        void AddWord(EnglishWord englishWord);
        void DeleteWordById(int id);
    }
}