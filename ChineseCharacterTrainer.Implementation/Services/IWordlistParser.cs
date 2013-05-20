using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IWordlistParser
    {
        List<DictionaryEntry> Import(IEnumerable<string> lines);
    }
}