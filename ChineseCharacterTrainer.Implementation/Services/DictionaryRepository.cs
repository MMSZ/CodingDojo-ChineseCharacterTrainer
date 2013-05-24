using ChineseCharacterTrainer.Model;
using ChineseCharacterTrainer.ServiceApp.Persistence;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly IChineseTrainerContext _chineseTrainerContext;

        public DictionaryRepository(IChineseTrainerContext chineseTrainerContext)
        {
            _chineseTrainerContext = chineseTrainerContext;
        }

        public void Add(Dictionary dictionary)
        {
            _chineseTrainerContext.Add(dictionary);
            _chineseTrainerContext.SaveChanges();
        }

        public List<Dictionary> GetAll()
        {
            return _chineseTrainerContext.GetAll<Dictionary>();
        }
    }
}