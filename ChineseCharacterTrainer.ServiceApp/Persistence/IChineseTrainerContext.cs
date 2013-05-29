using System.Collections.Generic;
using System.Data.Entity;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.ServiceApp.Persistence
{
    public interface IChineseTrainerContext
    {
        int SaveChanges();
        List<T> GetAll<T>() where T : class;
        void Add<T>(T entity) where T : class;


        DbSet<Dictionary> Dictionaries { get; set; }
        DbSet<Highscore> Highscores { get; set; }
        DbSet<DictionaryEntry> DictionaryEntries { get; set; }
        DbSet<Translation> Translations { get; set; }
        DbSet<User> Users { get; set; } 
        
    }
}