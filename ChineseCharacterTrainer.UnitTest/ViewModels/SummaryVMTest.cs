using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.ViewModels;
using NUnit.Framework;
using System;

namespace ChineseCharacterTrainer.UnitTest.ViewModels
{
    public class SummaryVMTest
    {
        private ISummaryVM _objectUnderTest;

        private readonly QuestionResult _questionResult = new QuestionResult(1, 2, TimeSpan.FromSeconds(1));

        [SetUp]
        public void Initialize()
        {
            _objectUnderTest = new SummaryVM();
            _objectUnderTest.Initialize(_questionResult);
        }

        [Test]
        public void ShouldSetNumberOfCorrectAnswersWhenInitializing()
        {
            Assert.AreEqual(1, _objectUnderTest.NumberOfCorrectAnswers);
        }

        [Test]
        public void ShouldSetNumberOfIncorrectAnswersWhenInitializing()
        {
            Assert.AreEqual(2, _objectUnderTest.NumberOfIncorrectAnswers);
        }

        [Test]
        public void ShouldSetDurationWhenInitializing()
        {
            Assert.AreEqual(TimeSpan.FromSeconds(1), _objectUnderTest.Duration);
        }
    }
}
