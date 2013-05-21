using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IDictionaryRepository
    {
        void Add(Dictionary dictionary);
        List<Dictionary> GetAll();
    }
}