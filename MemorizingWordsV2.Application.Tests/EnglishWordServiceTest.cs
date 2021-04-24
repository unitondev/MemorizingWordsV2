using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemorizingWordsV2.Application.Interfaces.Repositories;
using MemorizingWordsV2.Application.Services;
using MemorizingWordsV2.Domain.Models;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace MemorizingWordsV2.Application.Tests
{
    public class EnglishWordServiceTest
    {
        [Fact]
        public async Task GetAllAsync_WhenCalled_ListOfWordsExpected()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var testWords = GetValidTestWords();
            mockUnitOfWork.Setup(work => work.EnglishWordRepository.GetAllAsync())
                .ReturnsAsync(testWords);
            var _sut = new EnglishWordService(mockUnitOfWork.Object);

            //act
            var result = await _sut.GetAllEnglishWordsAsync();

            //assert
            Assert.Equal(testWords, result);
        }
        
        public static IEnumerable<EnglishWord> GetValidTestWords()
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
                    }
                };
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
            var result = await _sut.AddAsync(englishWord);

            //assert
            Assert.False(result);
        }
        
        
        [Fact]
        public async Task AddAsync_NullInParameter_ExceptionExpected()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var _sut = new EnglishWordService(mockUnitOfWork.Object);
            
            //act
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.AddAsync(null));
        }
        
        
        [Fact]
        public async Task AddRangeAsync_TwoWordsNotContainedOneContained_TwoExpected() 
        {
            //arrange
            var englishWords = GetValidTestWords();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            foreach (var englishWord in englishWords)
            {
                if (englishWord.Id == 2)
                {
                    mockUnitOfWork.Setup(work => work.EnglishWordRepository.FirstOrDefaultAsync(englishWord))
                        .ReturnsAsync(englishWord);
                }
                else 
                    mockUnitOfWork.Setup(work => work.EnglishWordRepository.FirstOrDefaultAsync(englishWord))
                    .ReturnsAsync((EnglishWord) null);   
            }
            var _sut = new EnglishWordService(mockUnitOfWork.Object);
            
            //act
            var addedWordsCount = await _sut.AddRangeAsync(englishWords);

            //assert
            Assert.Equal(2, addedWordsCount);
        }
        
        
        [Fact]
        public async Task AddRangeAsync_NullListInParameters_ArgumentNullExcExpected() 
        {
            //arrange
            var englishWords = GetValidTestWords();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var _sut = new EnglishWordService(mockUnitOfWork.Object);
            
            //act
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.AddRangeAsync(null));
        }
        
        
        [Fact]
        public async Task AddRangeAsync_NullWordInList_ZeroExpected() 
        {
            //arrange
            var englishWords = new List<EnglishWord> {null};
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var _sut = new EnglishWordService(mockUnitOfWork.Object);
            
            //act
            var result = await _sut.AddRangeAsync(englishWords);

            //assert
            Assert.Equal(0, result);
        }

        
        [Theory]
        [MemberData(nameof(GetTestWordInMethod))]
        public async Task DeleteAsync_ContainedWord_TrueExpected(EnglishWord englishWord) 
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(work => work.EnglishWordRepository.GetByIdOrDefaultAsync(englishWord.Id))
                .ReturnsAsync(englishWord);
            var _sut = new EnglishWordService(mockUnitOfWork.Object);
            
            //act
            var result = await _sut.DeleteByIdAsync(englishWord.Id);

            //assert
            Assert.True(result);
        }
        
        [Theory]
        [MemberData(nameof(GetTestWordInMethod))]
        public async Task DeleteAsync_NotContainedWord_FalseExpected(EnglishWord englishWord) 
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(work => work.EnglishWordRepository.GetByIdOrDefaultAsync(englishWord.Id))
                .ReturnsAsync((EnglishWord) null);
            var _sut = new EnglishWordService(mockUnitOfWork.Object);
            
            //act
            var result = await _sut.DeleteByIdAsync(englishWord.Id);

            //assert
            Assert.False(result);
        }
    }
}
