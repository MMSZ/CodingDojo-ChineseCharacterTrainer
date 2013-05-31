using ChineseCharacterTrainer.Model;
using System;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.ServiceApp.Persistence
{
    public interface IChineseTrainerContext
    {
        int SaveChanges();
        List<Entity> GetAll(Type type);
        void Add(Type type, Entity entity);
    }
}