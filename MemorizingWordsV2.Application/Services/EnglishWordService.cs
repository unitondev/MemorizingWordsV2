using System;
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
        public async Task<bool> AddAsync(EnglishWord item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Parameter item can't be null");
            }
            
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
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "Parameter items can't be null");
            }
            
            List<EnglishWord> wordsThatsNotContained = new List<EnglishWord>();
            bool isWordContained;
            
            foreach (var englishWord in items)
            {
                if (englishWord == null)
                {
                    break;
                }
                
                isWordContained =  await _unitOfWork.EnglishWordRepository.FirstOrDefaultAsync(englishWord) != null;
                if(!isWordContained)
                    wordsThatsNotContained.Add(englishWord);
            }

            if (wordsThatsNotContained.Count != 0)
            {
                await _unitOfWork.EnglishWordRepository.AddRangeAsync(wordsThatsNotContained);
                await _unitOfWork.CommitAsync();
            }
            
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