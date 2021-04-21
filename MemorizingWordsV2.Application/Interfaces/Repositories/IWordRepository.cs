using System.Collections.Generic;
using MemorizingWordsV2.Domain.Models;

namespace MemorizingWordsV2.Application.Interfaces.Repositories
{
    public interface IWordRepository<T> where T : class
    {
        T GetWordById(int id);
        List<T> GetAllWords();
        void AddWord(T word);
        void DeleteWordById(int id);
    }
}