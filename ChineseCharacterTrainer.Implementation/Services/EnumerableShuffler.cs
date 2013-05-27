using System;
using System.Collections.Generic;
using System.Linq;
using ChineseCharacterTrainer.Implementation.Utilities;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public class EnumerableShuffler : IEnumerableShuffler
    {
        private readonly Random _random;

        public EnumerableShuffler(IDateTime dateTime)
        {
            _random = new Random((int)dateTime.Now.Ticks);
        }

        public IEnumerable<T> Shuffle<T>(IEnumerable<T> enumerable)
        {
            if (enumerable == null) return new List<T>();

            var input = enumerable.ToList();
            var result = new List<T>();
            while (input.Count > 0)
            {
                var nextItemIndex = _random.Next(0, input.Count);
                var nextItem = input[nextItemIndex];
                result.Add(nextItem);
                input.Remove(nextItem);
            }

            return result;
        }
    }
}