using System;
using System.ServiceModel;
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
                throw;
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
                throw;
            }
        }

        public List<Highscore> GetHighscores()
        {
            var highscores = ChineseTrainerContext.GetAll<Highscore>();
            return highscores;
        }

        public List<Entity> GetAll(string typeName)
        {
            var result = ChineseTrainerContext.GetAll(Type.GetType(typeName));
            return result;
        }

        public void Add(string typeName, Entity entity)
        {
            ChineseTrainerContext.Add(Type.GetType(typeName), entity);
            ChineseTrainerContext.SaveChanges();
        }

        public void AddHighscore(Highscore highscore)
        {
            try
            {
                ChineseTrainerContext.Attach(highscore.Dictionary);
                ChineseTrainerContext.Add(highscore.User);
                ChineseTrainerContext.Add(highscore);
                ChineseTrainerContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
