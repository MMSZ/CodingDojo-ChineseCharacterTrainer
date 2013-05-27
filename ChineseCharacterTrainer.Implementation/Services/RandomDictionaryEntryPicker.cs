using System;
using System.Collections.Generic;
using System.Linq;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class RandomDictionaryEntryPicker : IDictionaryEntryPicker
    {
        private readonly IEnumerableShuffler _enumerableShuffler;
        private Queue<DictionaryEntry> _remainingEntries;
        private readonly List<DictionaryEntry> _queuedEntries = new List<DictionaryEntry>();

        public RandomDictionaryEntryPicker(IEnumerableShuffler enumerableShuffler)
        {
            _enumerableShuffler = enumerableShuffler;
            _remainingEntries = new Queue<DictionaryEntry>();
        }

        public void Initialize(IEnumerable<DictionaryEntry> entries)
        {
            if (entries == null) return;
            _remainingEntries = ShuffleEntries(entries.ToList());
        }

        public DictionaryEntry GetNextEntry()
        {
            if (_remainingEntries.Count == 0)
            {
                if (_queuedEntries.Count == 0)
                {
                    return null;
                }

                _remainingEntries = ShuffleEntries(_queuedEntries);
                _queuedEntries.Clear();
            }

            return _remainingEntries.Dequeue();
        }

        private Queue<DictionaryEntry> ShuffleEntries(IList<DictionaryEntry> queuedEntries)
        {
            return new Queue<DictionaryEntry>(_enumerableShuffler.Shuffle(queuedEntries));
        }

        public int NumberOfRemainingEntries { get { return _remainingEntries.Count + _queuedEntries.Count; } }

        public void QueueEntry(DictionaryEntry dictionaryEntry)
        {
            if (dictionaryEntry == null) return;
            _queuedEntries.Add(dictionaryEntry);
        }
    }
}
