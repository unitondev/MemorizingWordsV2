using System;
using System.Threading;
using System.Threading.Tasks;

namespace MemorizingWordsV2.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        public IEnglishWordRepository EnglishWordRepository { get; }
        public IRussianWordRepository RussianWordRepository { get; }
        
        
        public Task<int> CommitAsync();
        public Task<int> CommitAsync(CancellationToken cancellation);
    }
}