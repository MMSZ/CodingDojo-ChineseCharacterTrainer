using ChineseCharacterTrainer.Model;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IDictionaryRepository
    {
        void Add(Dictionary dictionary);
        List<Dictionary> GetAll();
    }
}