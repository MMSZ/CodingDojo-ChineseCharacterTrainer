using ChineseCharacterTrainer.Model;
using ChineseCharacterTrainer.ServiceApp.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ChineseCharacterTrainer.ServiceApp
{
    public class ChineseCharacterTrainerService : IChineseCharacterTrainerService
    {
        private readonly IChineseTrainerContext _chineseTrainerContext;

        public ChineseCharacterTrainerService()
        {
            Database.SetInitializer(new DontDropDbJustCreateTablesIfModelChanged<ChineseTrainerContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<ChineseTrainerContext>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<ChineseTrainerContext>());
            _chineseTrainerContext = new ChineseTrainerContext();
        }

        public void AddDictionary(Dictionary dictionary)
        {
            try
            {
                _chineseTrainerContext.Add(dictionary);
                _chineseTrainerContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public List<Dictionary> GetDictionaries()
        {
            var dictionaries = _chineseTrainerContext.GetAll<Dictionary>();
            return dictionaries;
        }
    }
}
