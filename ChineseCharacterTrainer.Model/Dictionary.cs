using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract(IsReference = true)]
    public class Dictionary : Entity
    {
        public Dictionary(string name, List<DictionaryEntry> entries)
        {
            Name = name;
            Entries = entries;
            if (Entries != null) Entries.ForEach(p =>
                                                     {
                                                         p.Dictionary = this;
                                                         p.DictionaryId = Id;
                                                     });
            Highscores = new List<Highscore>();
        }

        protected Dictionary() { }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public virtual List<DictionaryEntry> Entries { get; private set; }

        [DataMember]
        public virtual List<Highscore> Highscores { get; private set; }
    }
}
