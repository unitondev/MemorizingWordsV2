using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MemorizingWordsV2.Application.Interfaces.Services;
using MemorizingWordsV2.Application.Services;
using MemorizingWordsV2.ConsoleApplication.Interfaces;
using MemorizingWordsV2.Domain.Models;
using MemorizingWordsV2.Infrastructure;
using MemorizingWordsV2.Infrastructure.Repositories;

namespace MemorizingWordsV2.ConsoleApplication
{
    public class Application : IApplication
    {
        private bool _state;
        private readonly Random _random;
        private int _from;
        private int _to;
        private readonly IRussianWordService _russianWordService;
        
        public Application()
        {
            _state = true;
            _random = new Random();
            _russianWordService = new RussianWordService(new UnitOfWork(new WordsDBContext()));
        }

        public Application Initialize()
        {
            SetStateToTrue();
            CenterText(new string('=', 30));
            CenterText("Hello");
            CenterText("MemorizingWords v0.1");
            CenterText("Enter 't' for see translate, 'x' to exit, any button for continue ");
            CenterText("Let's get started");
            CenterText(new string('=', 30) + "\n");
            return this;
        }

        public bool IsStateTrue()
        {
            return _state;
        }

        public void SetStateToFalse()
        {
            _state = false;
        }
        
        public void SetStateToTrue()
        {
            _state = true;
        }

        public int GetRandomIntFromTo(int from, int to)
        {
            return _random.Next(from, to);
        }
        
        public void SetTheLastWordId(int assignableId)
        {
            _to = assignableId + 1;
        }

        public void SetTheFirstWordId(int assignableId)
        {
            _from = assignableId;
        }

        public async Task UpdateFirstAndListId()
        {
            SetTheLastWordId(await _russianWordService.GetTheLastWordIdAsync());
            SetTheFirstWordId(await _russianWordService.GetTheFirstWordIdAsync());
        }

        public void ConsoleWordOutput(List<RussianWord> russianWords)
        {
            foreach (var word in russianWords)
            {
                foreach (var englishRussianWord in word.EnglishRussianWords)
                {
                    Console.Write("English word: " + englishRussianWord.English.Word);
                    HandleThePressedKey(CheckThePressedKey(), word);
                    //Console.WriteLine("\t Part of speech: " + englishRussianWord.English.PartOfSpeech.PartName);
                }
            }
        }

        public char CheckThePressedKey()
        {
            return Console.ReadKey(true).KeyChar;
        }

        public void HandleThePressedKey(char pressedKey, RussianWord word)
        {
            switch (pressedKey)
            {
                case 't':
                    Console.Write("\t\tRussian translate: - " + word.Word + "\n");
                    break;
                case 'x':
                    SetStateToFalse();
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }

        public async Task<List<RussianWord>> GetRelatedPairByRandomIdAsync()
        {
            int randomId = GetRandomIntFromTo(_from, _to);
            return await _russianWordService.GetRelatedPairByIdAsync(randomId);
        }

        public async Task Run()
        {
            await UpdateFirstAndListId();
            
            while (IsStateTrue())
            {
                var relatedPair = await GetRelatedPairByRandomIdAsync();
                ConsoleWordOutput(relatedPair);
            }

            Console.WriteLine();
            CenterText("Bye!");
        }

        
        static void CenterText(string text)
        {
            Console.WriteLine("{0," + ((Console.WindowWidth + text.Length)/2) + "}", text);
        }
        
    }
}