using System.Collections.Generic;

namespace ChineseCharacterTrainer.ServiceApp.Persistence
{
    public interface IChineseTrainerContext
    {
        int SaveChanges();
        List<T> GetAll<T>() where T : class;
        void Add<T>(T entity) where T : class;
    }
}