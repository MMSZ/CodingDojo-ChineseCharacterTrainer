using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract]
    [KnownType(typeof(DictionaryEntry))]
    public class Dictionary : Entity
    {
        public Dictionary(string name, List<DictionaryEntry> entries)
        {
            Name = name;
            Entries = entries;
        }

        protected Dictionary() { }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public virtual List<DictionaryEntry> Entries { get; set; }
    }
}
