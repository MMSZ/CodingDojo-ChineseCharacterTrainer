using System.Collections.Generic;
using System.Linq;
using ChineseCharacterTrainer.Implementation.Model;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class WordlistParser : IWordlistParser
    {
        public List<DictionaryEntry> Import(IEnumerable<string> lines)
        {
            var list = new List<DictionaryEntry>();
            if (lines == null) return list;

            foreach (var line in lines)
            {
                if (line == null) continue;
                var commaSeparatedWords = line.Split(',');
                if (!VerifyCommaSeparatedValues(commaSeparatedWords)) continue;
                
                var translations = commaSeparatedWords[2].Split(';').ToList();
                for (var i = 0; i < translations.Count; i++)
                {
                    translations[i] = translations[i].Trim();
                }

                translations.RemoveAll(string.IsNullOrWhiteSpace);

                if (!VerifyTranslations(translations)) continue;

                var chineseCharacters = commaSeparatedWords[0].Trim();
                var pinyin = commaSeparatedWords[1].Trim();
                
                list.Add(new DictionaryEntry(chineseCharacters, pinyin, translations));
            }

            return list;
        }

        private static bool VerifyTranslations(ICollection<string> translations)
        {
            if (translations.Count == 0) return false;
            return true;
        }

        private static bool VerifyCommaSeparatedValues(ICollection<string> commaSeparatedWords)
        {
            if (commaSeparatedWords.Count != 3) return false;
            if (commaSeparatedWords.Any(string.IsNullOrWhiteSpace)) return false;
            return true;
        }
    }
}