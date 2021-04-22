using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemorizingWordsV2.Application.Interfaces.Repositories;
using MemorizingWordsV2.Application.Interfaces.Services;
using MemorizingWordsV2.Domain.Models;

namespace MemorizingWordsV2.Application.Services
{
    public class EnglishWordService : IEnglishWordService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnglishWordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EnglishWord>> GetAllEnglishWordsAsync()
        {
            return await _unitOfWork.EnglishWordRepository.GetAllAsync();
        }

        public async Task<EnglishWord> GetByIdOrDefaultAsync(int id)
        {
            return await _unitOfWork.EnglishWordRepository.GetByIdOrDefaultAsync(id);
        }
    }
}