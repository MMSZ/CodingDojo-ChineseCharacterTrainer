using ChineseCharacterTrainer.Model;
using ChineseCharacterTrainer.ServiceApp.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ChineseCharacterTrainer.ServiceApp
{
    public class ChineseCharacterTrainerService : IChineseCharacterTrainerService
    {
        private readonly IChineseTrainerContext _chineseTrainerContext;

        public ChineseCharacterTrainerService()
        {
            //Database.SetInitializer(new DontDropDbJustCreateTablesIfModelChanged<ChineseTrainerContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<ChineseTrainerContext>());
            Database.SetInitializer(new CreateDatabaseIfNotExists<ChineseTrainerContext>());
            _chineseTrainerContext = new ChineseTrainerContext();
        }

        public void AddDictionary(Dictionary dictionary)
        {
            try
            {
                _chineseTrainerContext.Add(dictionary);
                foreach (var dictionaryEntry in dictionary.Entries)
                {
                    _chineseTrainerContext.Add(dictionaryEntry);
                    foreach (var translation in dictionaryEntry.Translations)
                    {
                        _chineseTrainerContext.Add(translation);
                    }
                }

                _chineseTrainerContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
