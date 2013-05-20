using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Model
{
    public class DictionaryEntry
    {
        public DictionaryEntry(string chineseCharacters, string pinyin, List<string> translations)
        {
            ChineseCharacters = chineseCharacters;
            Pinyin = pinyin;
            Translations = translations;
        }

        public string ChineseCharacters { get; private set; }
        public string Pinyin { get; private set; }
        public List<string> Translations { get; set; }
    }
}
