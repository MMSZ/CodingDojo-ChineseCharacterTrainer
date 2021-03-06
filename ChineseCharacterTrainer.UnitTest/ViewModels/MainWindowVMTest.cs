﻿using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.ViewModels;
using ChineseCharacterTrainer.Model;
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
        private Mock<IHighscoreVM> _highscoreVMMock;

        [SetUp]
        public void Initialize()
        {
            _menuVMMock = new Mock<IMenuVM>();
            _questionVMMock = new Mock<IQuestionVM>();
            _summaryVMMock = new Mock<ISummaryVM>();
            _highscoreVMMock = new Mock<IHighscoreVM>();

            _objectUnderTest = new MainWindowVM(
                _menuVMMock.Object,
                _questionVMMock.Object,
                _summaryVMMock.Object,
                _highscoreVMMock.Object);
        }

        [Test]
        public void ShouldSetContentToMenuViewModelInConstructor()
        {
            Assert.AreEqual(_menuVMMock.Object, _objectUnderTest.Content);
        }

        [Test]
        public void ShouldSetContentToSummaryViewModelWhenQuestionsAreFinished()
        {
            _questionVMMock.Raise(p => p.QuestionsFinished += null,
                                  new QuestionResult(1, 2, TimeSpan.FromSeconds(1), 100));

            Assert.AreEqual(_summaryVMMock.Object, _objectUnderTest.Content);
        }

        [Test]
        public void ShouldInitializeSummaryViewModelWhenQuestionsAreFinished()
        {
            _menuVMMock.Setup(p => p.SelectedDictionary).Returns(new Dictionary("Test", null));
            var questionResult = new QuestionResult(1, 2, TimeSpan.FromSeconds(1), 100);

            _questionVMMock.Raise(p => p.QuestionsFinished += null, questionResult);

            _summaryVMMock.Verify(p => p.Initialize(_menuVMMock.Object.SelectedDictionary, questionResult), Times.Once());
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

        [Test]
        public void ShouldShowHighscoreWhenUploadFinishedIsRaised()
        {
            _objectUnderTest.Content = null;

            _summaryVMMock.Raise(p => p.UploadFinished += null,
                                 new Highscore(new User("Frank"), new Dictionary("Dict", null), 0));

            Assert.AreEqual(_objectUnderTest.Content, _highscoreVMMock.Object);
        }

        [Test]
        public void ShouldShowMenuWhenHighscoreIsFinished()
        {
            _objectUnderTest.Content = null;

            _highscoreVMMock.Raise(p => p.ReturnToMenuRequested += null);

            Assert.AreEqual(_objectUnderTest.Content, _menuVMMock.Object);
        }

        [Test]
        public void ShouldInitializeMenuWhenInitializing()
        {
            _objectUnderTest.Initialize();

            _menuVMMock.Verify(p => p.Initialize());
        }
    }
}
