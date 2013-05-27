using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(DictionaryEntry))]
    public class Dictionary : Entity
    {
        public Dictionary(string name, List<DictionaryEntry> entries)
        {
            Name = name;
            Entries = entries;
            if (Entries != null) Entries.ForEach(p => p.Dictionary = this);
        }

        protected Dictionary() { }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public virtual List<DictionaryEntry> Entries { get; set; }
    }
}
