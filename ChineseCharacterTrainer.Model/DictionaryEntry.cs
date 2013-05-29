using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract(IsReference = true)]
    public class DictionaryEntry : Entity
    {
        public DictionaryEntry(string chineseCharacters, string pinyin, List<Translation> translations)
        {
            ChineseCharacters = chineseCharacters;
            Pinyin = pinyin;
            Translations = translations;
            if(Translations != null) Translations.ForEach(p => p.DictionaryEntry = this);
        }

        protected DictionaryEntry() { }

        [DataMember]
        public string ChineseCharacters { get; private set; }

        [DataMember]
        public string Pinyin { get; private set; }

        [DataMember]
        public virtual List<Translation> Translations { get; private set; }

        [DataMember]
        public virtual Dictionary Dictionary { get; internal set; }
    }
}
