using System;
using System.Data.Objects;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using ChineseCharacterTrainer.Model;
using ChineseCharacterTrainer.ServiceApp.Persistence;
using System.Collections.Generic;
using System.Data.Entity;

namespace ChineseCharacterTrainer.ServiceApp
{
    public class ChineseCharacterTrainerService : IChineseCharacterTrainerService
    {
        private readonly IChineseTrainerContext _chineseTrainerContext;

        public ChineseCharacterTrainerService()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ChineseTrainerContext>());

            _chineseTrainerContext = new ChineseTrainerContext("DictionaryDB");
        }

        public void AddDictionary(Dictionary dictionary)
        {
            _chineseTrainerContext.Add(dictionary);
            _chineseTrainerContext.SaveChanges();
        }

        public List<Dictionary> GetDictionaries()
        {
            var dictionaries = _chineseTrainerContext.GetAll<Dictionary>();
            return dictionaries;
        }
    }

    public class ApplyDataContractResolverAttribute : Attribute, IOperationBehavior
    {
        #region IOperationBehavior Members

        public void AddBindingParameters(OperationDescription description, BindingParameterCollection parameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription description, ClientOperation proxy)
        {
            var dataContractSerializerOperationBehavior =
                description.Behaviors.Find<DataContractSerializerOperationBehavior>();
            dataContractSerializerOperationBehavior.DataContractResolver =
                new ProxyDataContractResolver();
        }

        public void ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
        {
            var dataContractSerializerOperationBehavior =
                description.Behaviors.Find<DataContractSerializerOperationBehavior>();
            dataContractSerializerOperationBehavior.DataContractResolver =
                new ProxyDataContractResolver();
        }

        public void Validate(OperationDescription description)
        {
            // Do validation.
        }

        #endregion
    }
}
