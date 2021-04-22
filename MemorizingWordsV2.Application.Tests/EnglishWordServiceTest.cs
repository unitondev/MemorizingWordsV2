using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemorizingWordsV2.Application.Interfaces.Repositories;
using MemorizingWordsV2.Application.Services;
using MemorizingWordsV2.Domain.Models;
using Moq;
using Xunit;

namespace MemorizingWordsV2.Application.Tests
{
    public class EnglishWordServiceTest
    {
        [Fact]
        public async Task GetAllAsync_WhenCalled_ListOfWordsExpected()
        {
            //arrange
            var mock = new Mock<IUnitOfWork>();
            var testWords = GetTestWords();
            mock.Setup(work => work.EnglishWordRepository.GetAllAsync().Result).Returns(testWords);
            var _sut = new EnglishWordService(mock.Object);

            //act
            var result = await _sut.GetAllEnglishWordsAsync();

            //assert
            Assert.Equal(testWords, result);
        }
        
        [Fact]
        public async Task GetByIdOrDefaultAsync_WhenCalled_ListOfWordsExpected()
        {
            //arrange
            var mock = new Mock<IUnitOfWork>();
            var testWords = GetTestWords();
            mock.Setup(work => work.EnglishWordRepository.GetByIdOrDefaultAsync(1).Result).Returns(_englishWord);
            var _sut = new EnglishWordService(mock.Object);

            //act
            var result = await _sut.GetByIdOrDefaultAsync(1);

            //assert
            Assert.Equal(_englishWord, result);
        }
        
        private EnglishWord _englishWord = new EnglishWord()
        {
            Id = 1, Word = "definitely", CreatedAt = DateTime.Now, PartOfSpeechId = new PartOfSpeech().Id
        };
        
        private IEnumerable<EnglishWord> GetTestWords()
        {
            return new List<EnglishWord>()
            {
                new() {Id = 1, Word = "definitely", CreatedAt = DateTime.Now, PartOfSpeechId = new PartOfSpeech(){Id = 1}.Id},
                new() {Id = 2, Word = "especially ", CreatedAt = DateTime.Now, PartOfSpeechId = new PartOfSpeech(){Id = 1}.Id},
                new() {Id = 3, Word = "obviously ", CreatedAt = DateTime.Now, PartOfSpeechId = new PartOfSpeech(){Id = 1}.Id},
            };
        }
    }
}