using System.Threading;
using System.Threading.Tasks;
using MemorizingWordsV2.Application.Interfaces.Repositories;

namespace MemorizingWordsV2.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WordsDBContext _dbContext;
        private IEnglishWordRepository _englishWordRepository { get; set; }

        public UnitOfWork(WordsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnglishWordRepository EnglishWordRepository
        {
            get
            {
                if (_englishWordRepository == null)
                    _englishWordRepository = new EnglishWordRepository(_dbContext);

                return _englishWordRepository;
            }
        }

        public async Task<int> CommitAsync()
        {
            return await CommitAsync(CancellationToken.None);
        }

        public async Task<int> CommitAsync(CancellationToken cancellation)
        {
            return await _dbContext.SaveChangesAsync(cancellation);
        }
        
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}