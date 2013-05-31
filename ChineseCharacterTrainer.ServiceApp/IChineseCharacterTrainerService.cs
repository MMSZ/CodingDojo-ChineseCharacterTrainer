using ChineseCharacterTrainer.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace ChineseCharacterTrainer.ServiceApp
{
    [ServiceContract]
    public interface IChineseCharacterTrainerService
    {
        [OperationContract]
        [ApplyDataContractResolver]
        List<Entity> GetAll(string typeName);

        [OperationContract]
        void Add(string typeName, Entity entity);
    }
}
