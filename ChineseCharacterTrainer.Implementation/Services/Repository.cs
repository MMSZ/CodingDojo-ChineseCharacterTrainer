using ChineseCharacterTrainer.Implementation.ServiceReference;
using ChineseCharacterTrainer.Model;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class Repository : IRepository
    {
        private readonly IChineseCharacterTrainerService _chineseCharacterTrainerService;

        public Repository(IChineseCharacterTrainerService chineseCharacterTrainerService)
        {
            _chineseCharacterTrainerService = chineseCharacterTrainerService;
        }

        public void AddDictionary(Dictionary dictionary)
        {
            _chineseCharacterTrainerService.AddDictionary(dictionary);
        }

        public List<Dictionary> GetDictionaries()
        {
            return _chineseCharacterTrainerService.GetDictionaries();
        }

        public void AddHighscore(Highscore highscore)
        {
            _chineseCharacterTrainerService.AddHighscore(highscore);
        }

        public List<Highscore> GetHighscores()
        {
            return _chineseCharacterTrainerService.GetHighscores();
        }
    }
}