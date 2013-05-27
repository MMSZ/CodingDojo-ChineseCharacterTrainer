using ChineseCharacterTrainer.Model;
using ChineseCharacterTrainer.ServiceApp.Persistence;
using System.Collections.Generic;
using System.Data.Entity;

namespace ChineseCharacterTrainer.ServiceApp
{
    public class ChineseCharacterTrainerService : IChineseCharacterTrainerService
    {
        private IChineseTrainerContext _chineseTrainerContext;

        public IChineseTrainerContext ChineseTrainerContext
        {
            get
            {
                return _chineseTrainerContext ??
                       (_chineseTrainerContext = new ChineseTrainerContext());
            }

            set { _chineseTrainerContext = value; }
        }

        public ChineseCharacterTrainerService()
        {
            Database.SetInitializer(new DontDropDbJustCreateTablesIfModelChanged<ChineseTrainerContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<ChineseTrainerContext>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<ChineseTrainerContext>());
        }

        public void AddDictionary(Dictionary dictionary)
        {
            ChineseTrainerContext.Add(dictionary);
            ChineseTrainerContext.SaveChanges();
        }

        public List<Dictionary> GetDictionaries()
        {
            var dictionaries = ChineseTrainerContext.GetAll<Dictionary>();
            return dictionaries;
        }
    }
}
