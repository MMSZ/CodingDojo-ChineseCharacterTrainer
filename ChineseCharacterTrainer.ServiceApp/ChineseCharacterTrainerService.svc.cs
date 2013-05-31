using ChineseCharacterTrainer.Model;
using ChineseCharacterTrainer.ServiceApp.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ChineseCharacterTrainer.ServiceApp
{
    public class ChineseCharacterTrainerService : IChineseCharacterTrainerService
    {
        private IChineseTrainerContext _chineseTrainerContext;

        public IChineseTrainerContext ChineseTrainerContext
        {
            get
            {
                return _chineseTrainerContext ??
                       (_chineseTrainerContext = new ChineseTrainerContext());
            }

            set { _chineseTrainerContext = value; }
        }

        public ChineseCharacterTrainerService()
        {
            Database.SetInitializer(new DontDropExistingDbCreateTablesIfModelChanged<ChineseTrainerContext>());
        }

        public List<Entity> GetAll(string typeName)
        {
            var result = ChineseTrainerContext.GetAll(Type.GetType(typeName));
            return result;
        }

        public void Add(string typeName, Entity entity)
        {
            ChineseTrainerContext.Add(Type.GetType(typeName), entity);
            ChineseTrainerContext.SaveChanges();
        }
    }
}
