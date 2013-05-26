using System;
using System.Collections.Generic;
using System.Linq;
using ChineseCharacterTrainer.Model;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class RandomDictionaryEntryPicker : IDictionaryEntryPicker
    {
        private Queue<DictionaryEntry> _remainingEntries;

        private readonly List<DictionaryEntry> _queuedEntries = new List<DictionaryEntry>(); 

        private readonly Random _random = new Random((int)DateTime.Now.Ticks);
 
        public void Initialize(IEnumerable<DictionaryEntry> entries)
        {
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

        private Queue<DictionaryEntry> ShuffleEntries(List<DictionaryEntry> queuedEntries)
        {
            var result = new Queue<DictionaryEntry>();
            while (queuedEntries.Count > 0)
            {
                var nextItemIndex = _random.Next(0, queuedEntries.Count);
                var nextItem = queuedEntries[nextItemIndex];
                result.Enqueue(nextItem);
                queuedEntries.Remove(nextItem);
            }

            return result;
        }

        public int NumberOfRemainingEntries { get; private set; }
        public void QueueEntry(DictionaryEntry dictionaryEntry)
        {
            _queuedEntries.Add(dictionaryEntry);
        }
    }
}
