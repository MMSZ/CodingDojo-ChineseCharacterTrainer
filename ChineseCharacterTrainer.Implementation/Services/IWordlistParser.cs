using ChineseCharacterTrainer.Model;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IWordlistParser
    {
        List<DictionaryEntry> Import(IEnumerable<string> lines);
    }
}