using System;
using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Implementation.Utilities;
using ChineseCharacterTrainer.Implementation.ViewModels;
using ChineseCharacterTrainer.Model;
using Moq;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.ViewModels
{
    public class QuestionVMTest
    {
        private QuestionVM _objectUnderTest;

        private Mock<IDateTime> _dateTimeMock;
        private Mock<IDictionaryEntryPicker> _dictionaryEntryPickerMock;
        private Mock<IScoreCalculator> _scoreCalculatorMock;

        private readonly DateTime _startTime = new DateTime(2010, 1, 1);
        private readonly DateTime _endTime = new DateTime(2010, 1, 2);

        private readonly List<DictionaryEntry> _dictionaryEntries = new List<DictionaryEntry>
            {
                new DictionaryEntry("你", "ni3", new List<Translation>{ new Translation("you")}),
                new DictionaryEntry("车", "che1", new List<Translation>{ new Translation("vehicle"), new Translation("car")})
            };

        [SetUp]
        public void Initialize()
        {
            _dateTimeMock = new Mock<IDateTime>();
            _dateTimeMock.Setup(p => p.Now).Returns(_startTime);

            var entryQueue = new Queue<DictionaryEntry>(_dictionaryEntries);
            _dictionaryEntryPickerMock = new Mock<IDictionaryEntryPicker>();
            _dictionaryEntryPickerMock.Setup(p => p.GetNextEntry()).Returns(
                () => entryQueue.Count > 0 ? entryQueue.Dequeue() : null);

            _scoreCalculatorMock = new Mock<IScoreCalculator>();
            _scoreCalculatorMock.Setup(p => p.CalculateScore(It.IsAny<TimeSpan>(), It.IsAny<int>())).Returns(10);

            _objectUnderTest = new QuestionVM(_dateTimeMock.Object, _dictionaryEntryPickerMock.Object,
                                              _scoreCalculatorMock.Object);

            _objectUnderTest.Initialize(_dictionaryEntries);
        }

        [Test]
        public void ShouldSetCurrentDictionaryEntryToFirstItemWhenInitializing()
        {
            Assert.AreEqual(_dictionaryEntries[0], _objectUnderTest.CurrentEntry);
        }

        [Test]
        public void ShouldGetCurrentTimeWhenInitializing()
        {
            _dateTimeMock.Verify(p => p.Now, Times.Once());
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
            AnswerAllQuestions();

            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
        }

        [Test]
        public void ShouldRaiseEventWhenNoMoreCharactersAreLeft()
        {
            QuestionResult questionResult = null;
            _objectUnderTest.QuestionsFinished += result => { questionResult = result; };

            AnswerAllQuestions();

            Assert.IsNotNull(questionResult);
        }

        [Test]
        public void ShouldRaiseEventWithNumberOfCorrectQuestions()
        {
            QuestionResult questionResult = null;
            _objectUnderTest.QuestionsFinished += result => { questionResult = result; };

            AnswerAllQuestionsCorrect();

            Assert.AreEqual(2, questionResult.NumberOfCorrectAnswers);
        }

        [Test]
        public void ShouldRaiseEventWithNumberOfIncorrectQuestions()
        {
            QuestionResult questionResult = null;
            _objectUnderTest.QuestionsFinished += result => { questionResult = result; };

            AnswerAllQuestions();

            Assert.AreEqual(2, questionResult.NumberOfIncorrectAnswers);
        }

        [Test]
        public void ShouldRaiseEventWithDuration()
        {
            _dateTimeMock.Setup(p => p.Now).Returns(_endTime);
            QuestionResult questionResult = null;
            _objectUnderTest.QuestionsFinished += result => { questionResult = result; };

            AnswerAllQuestions();

            Assert.AreEqual(TimeSpan.FromDays(1), questionResult.Duration);
        }

        [Test]
        public void ShouldIncreaseNumberOfCorrectAnswersAfterCorrectAnswer()
        {
            _objectUnderTest.Answer = "ni3";

            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.AreEqual(1, _objectUnderTest.NumberOfCorrectAnswers);
        }

        [Test]
        public void ShouldIncreaseNumberOfIncorrectAnswersAfterIncorrectAnswer()
        {
            _objectUnderTest.Answer = "wrong answer";

            _objectUnderTest.AnswerCommand.Execute(null);

            Assert.AreEqual(1, _objectUnderTest.NumberOfIncorrectAnswers);
        }

        [Test]
        public void ShouldQueueCurrentEntryIfAnsweredWrong()
        {
            _objectUnderTest.Answer = "wrong answer";

            _objectUnderTest.AnswerCommand.Execute(null);

            _dictionaryEntryPickerMock.Verify(p => p.QueueEntry(_objectUnderTest.CurrentEntry));
        }

        private void AnswerAllQuestions()
        {
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
        }

        private void AnswerAllQuestionsCorrect()
        {
            _objectUnderTest.Answer = "ni3";
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.Answer = "che1";
            _objectUnderTest.AnswerCommand.Execute(null);
            _objectUnderTest.AnswerCommand.Execute(null);
        }
    }
}
