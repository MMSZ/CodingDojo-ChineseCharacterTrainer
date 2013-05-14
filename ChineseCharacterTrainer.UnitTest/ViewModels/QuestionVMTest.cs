using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.ViewModels;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.ViewModels
{
    public class QuestionVMTest
    {
        private QuestionVM _objectUnderTest;

        private readonly List<DictionaryEntry> _dictionaryEntries = new List<DictionaryEntry>
            {
                new DictionaryEntry("你", "ni3"),
                new DictionaryEntry("车", "che1")
            };

        [SetUp]
        public void Initialize()
        {
            _objectUnderTest = new QuestionVM();
            _objectUnderTest.Initialize(_dictionaryEntries);

        }

        [Test]
        public void ShouldSetCurrentDictionaryEntryToFirstItemWhenInitializing()
        {
            Assert.AreEqual(_dictionaryEntries[0], _objectUnderTest.CurrentEntry);
        }

        [Test]
        public void ShouldGetNextEntryAfterAnswering()
        {
            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.AreEqual(_dictionaryEntries[1], _objectUnderTest.CurrentEntry);
        }
        
        [Test]
        public void ShouldSetCurrentEntryToNullWhenAllEntriesAreAnswered()
        {
            _objectUnderTest.AnswerCommand.Execute(null);

            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.AreEqual(null, _objectUnderTest.CurrentEntry);
        }
    }
}
