namespace ChineseCharacterTrainer.Implementation.Model
{
    public class DictionaryEntry
    {
        public DictionaryEntry(string chineseCharacters, string pinyin)
        {
            ChineseCharacters = chineseCharacters;
            Pinyin = pinyin;
        }

        public string ChineseCharacters { get; private set; }
        public string Pinyin { get; private set; }
    }
}
