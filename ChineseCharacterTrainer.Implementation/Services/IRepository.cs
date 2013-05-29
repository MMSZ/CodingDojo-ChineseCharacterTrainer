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
    }
}