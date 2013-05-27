using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Model;
using Moq;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.Services
{
    public class RandomDictionaryEntryPickerTest
    {
        private IDictionaryEntryPicker _objectUnderTest;
        private List<DictionaryEntry> _dictionaryEntries;
        private Mock<IEnumerableShuffler> _enumerableShufflerMock;

        [SetUp]
        public void Initialize()
        {
            _dictionaryEntries = new List<DictionaryEntry>
                {
                    new DictionaryEntry("你", "ni3", null),
                    new DictionaryEntry("走", "zou3", null)
                };

            _enumerableShufflerMock = new Mock<IEnumerableShuffler>();
            _enumerableShufflerMock.Setup(p => p.Shuffle(_dictionaryEntries)).Returns(_dictionaryEntries);

            _objectUnderTest = new RandomDictionaryEntryPicker(_enumerableShufflerMock.Object);
            _objectUnderTest.Initialize(_dictionaryEntries);
        }

        [Test]
        public void ShouldInitializeRemainingEntries()
        {
            Assert.AreEqual(2, _objectUnderTest.NumberOfRemainingEntries);
        }

        [Test]
        public void ShouldShuffleEntriesWhenInitializing()
        {
            _enumerableShufflerMock.Verify(p => p.Shuffle(_dictionaryEntries));
        }

        [Test]
        public void ShouldAddItemWhenQueueing()
        {
            _objectUnderTest.QueueEntry(new DictionaryEntry("好", "hao3", null));

            Assert.AreEqual(3, _objectUnderTest.NumberOfRemainingEntries);
        }

        [Test]
        public void ShouldRemoveItemAfterGettingNext()
        {
            _objectUnderTest.GetNextEntry();

            Assert.AreEqual(1, _objectUnderTest.NumberOfRemainingEntries);
        }

        [Test]
        public void ShouldNotQueueNull()
        {
            _objectUnderTest.QueueEntry(null);

            Assert.AreEqual(2, _objectUnderTest.NumberOfRemainingEntries);
        }

        [Test]
        public void ShouldGetQueuedEntryAfterInitialEntries()
        {

            var queuedEntry = new DictionaryEntry("好", "hao3", null);
            _enumerableShufflerMock.Setup(p => p.Shuffle(It.IsAny<List<DictionaryEntry>>()))
                                   .Returns(new List<DictionaryEntry> {queuedEntry});
            _objectUnderTest.QueueEntry(queuedEntry);
            _objectUnderTest.GetNextEntry();
            _objectUnderTest.GetNextEntry();

            var actual = _objectUnderTest.GetNextEntry();

            Assert.AreEqual(queuedEntry, actual);
        }

        [Test]
        public void ShouldShuffleEntriesAfterInitialEntries()
        {
            var queuedEntry = new DictionaryEntry("好", "hao3", null);
            _enumerableShufflerMock.Setup(p => p.Shuffle(It.IsAny<List<DictionaryEntry>>()))
                                   .Returns(new List<DictionaryEntry> { queuedEntry });
            _objectUnderTest.QueueEntry(queuedEntry);
            _objectUnderTest.GetNextEntry();
            _objectUnderTest.GetNextEntry();

            _objectUnderTest.GetNextEntry();

            _enumerableShufflerMock.Verify(p => p.Shuffle(It.IsAny<IEnumerable<DictionaryEntry>>()), Times.Exactly(2));
        }

        [Test]
        public void ShouldReturnNullWhenNoMoreEntriesAreLeft()
        {
            _objectUnderTest.GetNextEntry();
            _objectUnderTest.GetNextEntry();

            var actual = _objectUnderTest.GetNextEntry();

            Assert.IsNull(actual);
        }

        [Test]
        public void ShouldDoNothingWhenPassingNullToInitialize()
        {
            _objectUnderTest.Initialize(null);

            Assert.AreEqual(2, _objectUnderTest.NumberOfRemainingEntries);
        }
    }
}
