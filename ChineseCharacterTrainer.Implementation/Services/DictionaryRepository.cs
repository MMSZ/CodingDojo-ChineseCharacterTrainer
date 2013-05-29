using ChineseCharacterTrainer.Implementation.ServiceReference;
using ChineseCharacterTrainer.Model;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly IChineseCharacterTrainerService _chineseCharacterTrainerService;

        public DictionaryRepository(IChineseCharacterTrainerService chineseCharacterTrainerService)
        {
            _chineseCharacterTrainerService = chineseCharacterTrainerService;
        }

        public void Add(Dictionary dictionary)
        {
            _chineseCharacterTrainerService.AddDictionary(dictionary);
        }

        public List<Dictionary> GetAll()
        {
            return _chineseCharacterTrainerService.GetDictionaries();
        }
    }
}