using System;
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
            //Database.SetInitializer(new DontDropDbJustCreateTablesIfModelChanged<ChineseTrainerContext>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ChineseTrainerContext>());
            Database.SetInitializer(new DontDropExistingDbCreateTablesIfModelChanged<ChineseTrainerContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<ChineseTrainerContext>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<ChineseTrainerContext>());
        }

        public void AddDictionary(Dictionary dictionary)
        {
            try
            {
                ChineseTrainerContext.Add(dictionary);
                ChineseTrainerContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public List<Dictionary> GetDictionaries()
        {
            try
            {
                var dictionaries = ChineseTrainerContext.GetAll<Dictionary>();
                return dictionaries;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public List<Highscore> GetHighscores()
        {
            var highscores = ChineseTrainerContext.GetAll<Highscore>();
            return highscores;
        }

        public void AddHighscore(Highscore highscore)
        {
            try
            {
                ChineseTrainerContext.Add(highscore);
                ChineseTrainerContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
