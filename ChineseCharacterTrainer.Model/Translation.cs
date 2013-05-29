using System;
using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract(IsReference = true)]
    public class Translation : Entity
    {
        public Translation(string value)
        {
            Value = value;
        }

        protected Translation() { }

        [DataMember]
        public string Value { get; private set; }

        [DataMember]
        public Guid DictionaryEntryId { get; internal set; }

        [DataMember]
        public virtual DictionaryEntry DictionaryEntry { get; internal set; }
    }
}
