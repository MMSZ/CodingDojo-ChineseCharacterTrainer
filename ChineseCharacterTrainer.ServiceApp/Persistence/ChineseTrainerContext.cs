using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects.DataClasses;
using System.Linq;
using ChineseCharacterTrainer.Model;

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

        public void Attach<T>(T entity) where T : class
        {
            Set<T>().Attach(entity);
        }

        public List<Entity> GetAll(Type type)
        {
            var dbSet = Set(type);
            dbSet.Load();

            Type listType = typeof(List<>);
            Type[] typeArgs = { type };
            Type listTypeGenericRuntime = listType.MakeGenericType(typeArgs);
            var enumerable = Activator.CreateInstance(listTypeGenericRuntime, dbSet.Local) as IEnumerable;


            var result = new List<Entity>();
            foreach (var item in enumerable)
            {
                result.Add(item as Entity);
            }

            return result;
        }

        public void Add(Type type, Entity entity)
        {
            Set(type).Add(entity);
        }

        //public DbSet<Dictionary> Dictionaries { get; set; }
        //public DbSet<Highscore> Highscores { get; set; }
        //public DbSet<DictionaryEntry> DictionaryEntries { get; set; }
        //public DbSet<Translation> Translations { get; set; }
        //public DbSet<User> Users { get; set; }
    }
}
