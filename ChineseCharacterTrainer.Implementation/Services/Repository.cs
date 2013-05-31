using System.Linq;
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

        public List<T> GetAll<T>() where T : Entity
        {
            var result = _chineseCharacterTrainerService.GetAll(typeof (T).AssemblyQualifiedName);
            return result.Select(p => p as T).ToList();
        }

        public void Add<T>(T entity) where T : Entity
        {
            _chineseCharacterTrainerService.Add(typeof(T).AssemblyQualifiedName, entity);
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