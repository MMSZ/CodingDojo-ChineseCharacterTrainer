﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChineseCharacterTrainer.Implementation.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IChineseCharacterTrainerService")]
    public interface IChineseCharacterTrainerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChineseCharacterTrainerService/AddDictionary", ReplyAction="http://tempuri.org/IChineseCharacterTrainerService/AddDictionaryResponse")]
        void AddDictionary(ChineseCharacterTrainer.Model.Dictionary dictionary);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChineseCharacterTrainerService/GetDictionaries", ReplyAction="http://tempuri.org/IChineseCharacterTrainerService/GetDictionariesResponse")]
        System.Collections.Generic.List<ChineseCharacterTrainer.Model.Dictionary> GetDictionaries();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChineseCharacterTrainerService/UploadHighscore", ReplyAction="http://tempuri.org/IChineseCharacterTrainerService/UploadHighscoreResponse")]
        void UploadHighscore(ChineseCharacterTrainer.Model.Highscore highscore);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChineseCharacterTrainerServiceChannel : ChineseCharacterTrainer.Implementation.ServiceReference.IChineseCharacterTrainerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ChineseCharacterTrainerServiceClient : System.ServiceModel.ClientBase<ChineseCharacterTrainer.Implementation.ServiceReference.IChineseCharacterTrainerService>, ChineseCharacterTrainer.Implementation.ServiceReference.IChineseCharacterTrainerService {
        
        public ChineseCharacterTrainerServiceClient() {
        }
        
        public ChineseCharacterTrainerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ChineseCharacterTrainerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ChineseCharacterTrainerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ChineseCharacterTrainerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddDictionary(ChineseCharacterTrainer.Model.Dictionary dictionary) {
            base.Channel.AddDictionary(dictionary);
        }
        
        public System.Collections.Generic.List<ChineseCharacterTrainer.Model.Dictionary> GetDictionaries() {
            return base.Channel.GetDictionaries();
        }
        
        public void UploadHighscore(ChineseCharacterTrainer.Model.Highscore highscore) {
            base.Channel.UploadHighscore(highscore);
        }
    }
}
