using ChineseCharacterTrainer.Model;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IRepository
    {
        void AddDictionary(Dictionary dictionary);
        List<Dictionary> GetDictionaries();

        void AddHighscore(Highscore highscore);
        List<Highscore> GetHighscores();
        List<T> GetAll<T>() where T : Entity;
        void Add<T>(T entity) where T : Entity;
    }
}