using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Model
{
    public class Dictionary : Entity
    {
        public Dictionary(string name, List<DictionaryEntry> entries)
        {
            Name = name;
            Entries = entries;
        }

        protected Dictionary() { }

        public string Name { get; set; }
        public virtual List<DictionaryEntry> Entries { get; set; }
    }
}
