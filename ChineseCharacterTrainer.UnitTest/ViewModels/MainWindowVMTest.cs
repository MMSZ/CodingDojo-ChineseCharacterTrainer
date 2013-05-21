using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.ViewModels;
using Moq;
using NUnit.Framework;
using System;

namespace ChineseCharacterTrainer.UnitTest.ViewModels
{
    public class MainWindowVMTest
    {
        private IMainWindowVM _objectUnderTest;
        private Mock<IQuestionVM> _questionVMMock;
        private Mock<ISummaryVM> _summaryVMMock;
        private Mock<IMenuVM> _menuVMMock;



        [SetUp]
        public void Initialize()
        {
            _menuVMMock = new Mock<IMenuVM>();
            _questionVMMock = new Mock<IQuestionVM>();
            _summaryVMMock = new Mock<ISummaryVM>();


            _objectUnderTest = new MainWindowVM(_menuVMMock.Object, _questionVMMock.Object, _summaryVMMock.Object);
        }

        [Test]
        public void ShouldSetContentToMenuViewModelInConstructor()
        {
            Assert.AreEqual(_menuVMMock.Object, _objectUnderTest.Content);
        }

        [Test]
        public void ShouldSetContentToSummaryViewModelWhenQuestionsAreFinished()
        {
            _questionVMMock.Raise(p => p.QuestionsFinished += null, new QuestionResult(1, 2, TimeSpan.FromSeconds(1)));

            Assert.AreEqual(_summaryVMMock.Object, _objectUnderTest.Content);
        }

        [Test]
        public void ShouldInitializeSummaryViewModelWhenQuestionsAreFinished()
        {
            var questionResult = new QuestionResult(1, 2, TimeSpan.FromSeconds(1));
            _questionVMMock.Raise(p => p.QuestionsFinished += null, questionResult);

            _summaryVMMock.Verify(p => p.Initialize(questionResult), Times.Once());
        }

        [Test]
        public void ShouldInitializeQuestionVMWhenDictionaryOpenIsRequested()
        {
            var entries = new List<DictionaryEntry>();
            var dictionary = new Dictionary("1", entries);
            _menuVMMock.Raise(p => p.OpenDictionaryRequested += null, dictionary);

            _questionVMMock.Verify(p => p.Initialize(dictionary.Entries));
        }

        [Test]
        public void ShouldStartTrainingWhenDictionaryOpenIsRequested()
        {
            var dictionary = new Dictionary("1", null);
            _menuVMMock.Raise(p => p.OpenDictionaryRequested += null, dictionary);

            Assert.AreEqual(_questionVMMock.Object, _objectUnderTest.Content);
        }
    }
}
