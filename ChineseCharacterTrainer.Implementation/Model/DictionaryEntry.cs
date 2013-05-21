using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Model
{
    public class DictionaryEntry : Entity
    {
        public DictionaryEntry(string chineseCharacters, string pinyin, List<Translation> translations)
        {
            ChineseCharacters = chineseCharacters;
            Pinyin = pinyin;
            Translations = translations;
        }

        protected DictionaryEntry() { }

        public string ChineseCharacters { get; private set; }
        public string Pinyin { get; private set; }
        public List<Translation> Translations { get; set; }
    }
}
