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
        public void ShouldGetNextEntryAfterAccepting()
        {
            _objectUnderTest.AnswerCommand.Execute(null);

            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.AreEqual(_dictionaryEntries[1], _objectUnderTest.CurrentEntry);
        }
        
        [Test]
        public void ShouldSetCurrentEntryToNullWhenAllEntriesAreAnswered()
        {
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);

            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.AreEqual(null, _objectUnderTest.CurrentEntry);
        }

        [Test]
        public void ShouldResetAnswerAfterAccepting()
        {
            _objectUnderTest.Answer = "SomeAnswer";
            _objectUnderTest.AnswerCommand.Execute(null);

            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.AreEqual(string.Empty, _objectUnderTest.Answer);
        }

        [Test]
        public void ShouldNotResetAnswerAfterAccepting()
        {
            _objectUnderTest.Answer = "SomeAnswer";

            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.AreEqual("SomeAnswer", _objectUnderTest.Answer);
        }
        
        [Test]
        public void ShouldInitializeAnswerToEmptyString()
        {
            Assert.AreEqual(string.Empty, _objectUnderTest.Answer);
        }

        [Test]
        public void ShouldSwitchToAcceptModeAfterAnswering()
        {
            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.IsFalse(_objectUnderTest.IsInAnswerMode);
        }

        [Test]
        public void ShouldSwitchToAnswerModeAfterAccepting()
        {
            _objectUnderTest.AnswerCommand.Execute(null);

            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.IsTrue(_objectUnderTest.IsInAnswerMode);
        }

        [Test]
        public void ShouldSetLastAnswerCorrectIfItWasCorrect()
        {
            _objectUnderTest.Answer = "ni3";

            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.IsTrue(_objectUnderTest.LastAnswerWasCorrect);
        }

        [Test]
        public void ShouldSetLastAnswerIncorrectIfItWasIncorrect()
        {
            _objectUnderTest.Answer = "wrong answer";

            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.IsFalse(_objectUnderTest.LastAnswerWasCorrect);
        }

        [Test]
        public void ShouldNotFailWhenAnsweringAfterDictionaryIsEmpty()
        {
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
        }
    }
}
