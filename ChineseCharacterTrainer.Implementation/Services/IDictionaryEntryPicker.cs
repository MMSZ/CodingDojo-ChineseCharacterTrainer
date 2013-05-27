using System.Collections.Generic;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IDictionaryEntryPicker
    {
        void Initialize(IEnumerable<DictionaryEntry> entries);
        DictionaryEntry GetNextEntry();
        int NumberOfRemainingEntries { get; }
        void QueueEntry(DictionaryEntry dictionaryEntry);
    }
}