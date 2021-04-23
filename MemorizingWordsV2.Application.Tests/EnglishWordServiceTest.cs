using System;
using System.Collections;
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
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var testWords = GetTestWords();
            mockUnitOfWork.Setup(work => work.EnglishWordRepository.GetAllAsync())
                .ReturnsAsync(testWords);
            var _sut = new EnglishWordService(mockUnitOfWork.Object);

            //act
            var result = await _sut.GetAllEnglishWordsAsync();

            //assert
            Assert.Equal(testWords, result);
        }
        
        private IEnumerable<EnglishWord> GetTestWords()
        {
            return new List<EnglishWord>()
            {
                new() {Id = 1, Word = "definitely", CreatedAt = DateTime.Now, PartOfSpeechId = new PartOfSpeech(){Id = 1}.Id},
                new() {Id = 2, Word = "especially ", CreatedAt = DateTime.Now, PartOfSpeechId = new PartOfSpeech(){Id = 1}.Id},
                new() {Id = 3, Word = "obviously ", CreatedAt = DateTime.Now, PartOfSpeechId = new PartOfSpeech(){Id = 1}.Id},
            };
        }
        
        
        [Fact]
        public async Task GetByIdOrDefaultAsync_WordExists_ListOfWordsExpected()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(work => work.EnglishWordRepository.GetByIdOrDefaultAsync(1))
                .ReturnsAsync(_englishWord);
            var _sut = new EnglishWordService(mockUnitOfWork.Object);

            //act
            var result = await _sut.GetByIdOrDefaultAsync(1);

            //assert
            Assert.Equal(_englishWord, result);
        }

        public static EnglishWord _englishWord = new EnglishWord()
        {
            Id = 1, Word = "definitely", CreatedAt = DateTime.Now, PartOfSpeechId = new PartOfSpeech(){Id = 1}.Id
        };
        
        
        [Fact]
        public async Task GetByIdOrDefaultAsync_WordDosentExist_DefaultExpected()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(work => work.EnglishWordRepository.GetByIdOrDefaultAsync(1))
                .ReturnsAsync((EnglishWord) null);
            var _sut = new EnglishWordService(mockUnitOfWork.Object);

            //act
            var result = await _sut.GetByIdOrDefaultAsync(2);

            //assert
            Assert.Null(result);
        }

        
        [Theory]
        [MemberData(nameof(GetTestWordInMethod))]
        public async Task AddAsync_ThreeDifferentWords_TrueExpected(EnglishWord englishWord)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(work => work.EnglishWordRepository.FirstOrDefaultAsync(englishWord))
                .ReturnsAsync((EnglishWord) null);
            var _sut = new EnglishWordService(mockUnitOfWork.Object);
            
            //act
            var result = await _sut.AddAsync(englishWord);

            //assert
            Assert.True(result);
        }

        public static IEnumerable<object[]> GetTestWordInMethod()
        {
            yield return new object[]
                { 
                    new EnglishWord()
                {
                    Id = 1, Word = "definitely", CreatedAt = DateTime.Now, PartOfSpeechId = new PartOfSpeech(){Id = 1}.Id
                }};
            yield return new object[]
                { 
                    new EnglishWord()
                {
                    Id = 2, Word = "especially ", CreatedAt = DateTime.Now, PartOfSpeechId = new PartOfSpeech() {Id = 1}.Id
                }};
            yield return new object[]
                { 
                    new EnglishWord()
                {
                    Id = 3, Word = "obviously ", CreatedAt = DateTime.Now, PartOfSpeechId = new PartOfSpeech() {Id = 1}.Id
                }};
        }
        
        
        [Theory]
        [MemberData(nameof(GetTestWordInMethod))]
        public async Task AddAsync_TwoSameWords_FalseExpected(EnglishWord englishWord) 
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(work => work.EnglishWordRepository.FirstOrDefaultAsync(englishWord))
                .ReturnsAsync(englishWord);
            var _sut = new EnglishWordService(mockUnitOfWork.Object);
            
            //act
            await _sut.AddAsync(englishWord);
            var result = await _sut.AddAsync(englishWord);

            //assert
            Assert.False(result);
        }
        
    }
}
