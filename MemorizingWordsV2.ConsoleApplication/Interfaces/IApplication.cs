using System.Collections.Generic;
using System.Threading.Tasks;
using MemorizingWordsV2.Domain.Models;

namespace MemorizingWordsV2.ConsoleApplication.Interfaces
{
    public interface IApplication
    {
        Application Initialize();
        Task Run();
        bool IsStateTrue();
        void SetStateToTrue();
        void SetStateToFalse();
        int GetRandomIntFromTo(int from, int to);
        Task<List<RussianWord>> GetRelatedPairByRandomIdAsync();
        void SetTheLastWordId(int assignableId);
        void SetTheFirstWordId(int assignableId);
        Task UpdateFirstAndListId();
        void ConsoleWordOutput(List<RussianWord> russianWords);
        char CheckThePressedKey();
        void HandleThePressedKey(char pressedKey, RussianWord word);
    }
}