using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ChineseCharacterTrainer.Model
{
    [DataContract]
    [KnownType(typeof(Translation))]
    public class DictionaryEntry : Entity
    {
        public DictionaryEntry(string chineseCharacters, string pinyin, List<Translation> translations)
        {
            ChineseCharacters = chineseCharacters;
            Pinyin = pinyin;
            Translations = translations;
        }

        protected DictionaryEntry() { }

        [DataMember]
        public string ChineseCharacters { get; private set; }

        [DataMember]
        public string Pinyin { get; private set; }

        [DataMember]
        public List<Translation> Translations { get; set; }
    }
}
