using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MemorizingWordsV2.Application.Interfaces.Repositories;
using MemorizingWordsV2.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MemorizingWordsV2.Infrastructure.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : Model
    {
        private readonly WordsDBContext _dbContext;

        public RepositoryAsync(WordsDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<T> GetByIdOrDefaultAsync(int id)
        {
            return await GetByIdOrDefaultAsync(id, CancellationToken.None);
        }
        
        public async Task<T> GetByIdOrDefaultAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(model => model.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetAllAsync(CancellationToken.None);
        }
        
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(T item)
        {
            await AddAsync(item, CancellationToken.None);
        }
        
        public async Task AddAsync(T item, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddAsync(item, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> item)
        {
            await AddRangeAsync(item, CancellationToken.None);
        }
        
        public async Task AddRangeAsync(IEnumerable<T> item, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddRangeAsync(item, cancellationToken);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await DeleteByIdAsync(id, CancellationToken.None);
        }
        
        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity =  await _dbContext.EnglishWords.FirstOrDefaultAsync(
                model => model.Id == id, cancellationToken);
            _dbContext.EnglishWords.Remove(entity);
        }
    }
}