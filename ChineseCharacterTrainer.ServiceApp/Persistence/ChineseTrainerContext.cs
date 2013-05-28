using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChineseCharacterTrainer.ServiceApp.Persistence
{
    public class ChineseTrainerContext : DbContext, IChineseTrainerContext
    {
        public ChineseTrainerContext(string databaseName)
           : base("data source=localhost;initial catalog=" + databaseName + ";integrated security=True;multipleactiveresultsets=True;App=EntityFramework")
        {
        }

        public ChineseTrainerContext() { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new DictionaryEntryMapping());
            modelBuilder.Configurations.Add(new DictionaryMapping());
            modelBuilder.Configurations.Add(new TranslationMapping());
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new HighscoreMapping());
        }

        public List<T> GetAll<T>() where T : class
        {
            return Set<T>().ToList();
        }

        public void Add<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
        }
    }
}
