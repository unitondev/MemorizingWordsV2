using System.Collections.Generic;
using System.Threading.Tasks;
using MemorizingWordsV2.Domain.Models;

namespace MemorizingWordsV2.Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<T> where T : Model
    {
        Task<T> GetByIdOrDefaultAsync(int id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T item);
        Task AddRangeAsync(List<T> items);
        Task DeleteByIdAsync(int id);
    }
}