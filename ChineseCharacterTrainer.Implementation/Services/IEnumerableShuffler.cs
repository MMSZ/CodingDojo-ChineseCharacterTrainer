using System.Collections.Generic;

namespace ChineseCharacterTrainer.Implementation.Services
{
    public interface IEnumerableShuffler
    {
        IEnumerable<T> Shuffle<T>(IEnumerable<T> enumerable);
    }
}