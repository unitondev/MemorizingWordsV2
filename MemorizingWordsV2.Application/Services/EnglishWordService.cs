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
        
        public async Task<EnglishWord> GetByIdOrDefaultAsync(int id)
        {
            return await _unitOfWork.EnglishWordRepository.GetByIdOrDefaultAsync(id);
        }

        public async Task<IEnumerable<EnglishWord>> GetAllEnglishWordsAsync()
        {
            return await _unitOfWork.EnglishWordRepository.GetAllAsync();
        }
        
        //TODO CANCELLATION TOKEN
        //TODO maybe create method that fails should throw an exception?
        //Now create method that fails return false, otherwise true
        public async Task<bool> AddAsync(EnglishWord item)
        {
            bool isWordContained =  await _unitOfWork.EnglishWordRepository.FirstOrDefaultAsync(item) != null;
            
            if (!isWordContained)
            {
                await _unitOfWork.EnglishWordRepository.AddAsync(item);
                await _unitOfWork.CommitAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<int> AddRangeAsync(IEnumerable<EnglishWord> items)
        {
            List<EnglishWord> wordsThatsNotContained = new List<EnglishWord>();
            bool isWordContained;
            
            foreach (var englishWord in items)
            {
                isWordContained =  await _unitOfWork.EnglishWordRepository.FirstOrDefaultAsync(englishWord) != null;
                if(!isWordContained)
                    wordsThatsNotContained.Add(englishWord);
            }

            await _unitOfWork.EnglishWordRepository.AddRangeAsync(wordsThatsNotContained);
            await _unitOfWork.CommitAsync();
            return wordsThatsNotContained.Count;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            bool isWordContained = await _unitOfWork.EnglishWordRepository.GetByIdOrDefaultAsync(id) != null;

            if (isWordContained)
            {
                await _unitOfWork.EnglishWordRepository.DeleteByIdAsync(id);
                await _unitOfWork.CommitAsync();
                return true;
            }
            else
                return false;
        }
    }
}