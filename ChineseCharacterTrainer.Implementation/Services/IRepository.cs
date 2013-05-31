using ChineseCharacterTrainer.Model;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IRepository
    {
        List<T> GetAll<T>() where T : Entity;
        void Add<T>(T entity) where T : Entity;
    }
}