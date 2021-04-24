using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemorizingWordsV2.Application.Interfaces.Repositories;
using MemorizingWordsV2.Application.Interfaces.Services;
using MemorizingWordsV2.Domain.Models;

namespace MemorizingWordsV2.Application.Services
{
    public class RussianWordService : IRussianWordService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RussianWordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<List<RussianWord>> GetRelatedPairByIdAsync(int id)
        {
            var russianWord = await _unitOfWork.RussianWordRepository.GetByIdOrDefaultAsync(id);
            if (russianWord == null)
            {
                throw new ArgumentException($"Current id {id} doesn't exists", nameof(id));
            }
            return await _unitOfWork.RussianWordRepository.GetRelatedWordAndPartOfSpeechAsync(russianWord);
        }
        

        public async Task<bool> AddPairAsync(RussianWord russianWord, EnglishWord englishWord)
        {
            if (russianWord == null)
            {
                throw new ArgumentNullException(nameof(russianWord), "Parameter russianWord can't be null");
            }
            
            if (englishWord == null)
            {
                throw new ArgumentNullException(nameof(englishWord), "Parameter englishWord can't be null");
            }
            
            bool isWordContained =  await _unitOfWork.RussianWordRepository.FirstOrDefaultAsync(russianWord) != null;

            if (!isWordContained)
            {
                russianWord.EnglishRussianWords = new List<EnglishRussianWord>()
                {
                    new() {English = englishWord, Russian = russianWord}
                };

                await _unitOfWork.RussianWordRepository.AddAsync(russianWord);
                await _unitOfWork.CommitAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> AddPairRangeAsync(IEnumerable<RussianWord> russianWords, IEnumerable<EnglishWord> englishWords)
        {
            if (russianWords == null)
            {
                throw new ArgumentNullException(nameof(russianWords), "Parameter russianWords can't be null");
            }
            
            if (englishWords == null)
            {
                throw new ArgumentNullException(nameof(englishWords), "Parameter englishWords can't be null");
            }
            
            List<RussianWord> russiansWordsThatsNotContained = new List<RussianWord>();
            List<EnglishWord> englishWordsThatsNotContained = new List<EnglishWord>();
            bool isWordContained;

            for (int i = 0; i < russianWords.Count(); i++)  
            {
                if (russianWords.ElementAt(i) == null)
                {
                    break;
                }

                isWordContained = await _unitOfWork.RussianWordRepository.FirstOrDefaultAsync(russianWords.ElementAt(i)) != null;
                if (!isWordContained)
                {
                    russiansWordsThatsNotContained.Add(russianWords.ElementAt(i));
                    englishWordsThatsNotContained.Add(englishWords.ElementAt(i));
                }
            }

            if (russiansWordsThatsNotContained.Count != 0)
            {
                for (int i = 0; i < russiansWordsThatsNotContained.Count; i++)
                {
                    russiansWordsThatsNotContained[i].EnglishRussianWords = new List<EnglishRussianWord>()
                    {
                        new() {English = englishWordsThatsNotContained[i], Russian = russiansWordsThatsNotContained[i]}
                    };
                }
               
                await _unitOfWork.RussianWordRepository.AddRangeAsync(russiansWordsThatsNotContained);
                await _unitOfWork.CommitAsync();
            }
            
            return russiansWordsThatsNotContained.Count;
        }

        public async Task<bool> DeletePairByIdAsync(int id)
        {
            bool isWordContained = await _unitOfWork.RussianWordRepository.GetByIdOrDefaultAsync(id) != null;

            if (isWordContained)
            {
                await _unitOfWork.RussianWordRepository.DeleteByIdAsync(id);
                await _unitOfWork.CommitAsync();
                return true;
            }
            else
                return false;
        }
    }
}