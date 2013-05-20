using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Implementation.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.UnitTest.ViewModels
{
    public class MainWindowVMTest
    {
        private IMainWindowVM _objectUnderTest;
        private Mock<IQuestionVM> _questionVMMock;
        private Mock<ISummaryVM> _summaryVMMock;
        private Mock<IMenuVM> _menuVMMock;
        private Mock<ITextFileReader> _textFileReaderMock;
        private Mock<IWordlistParser> _wordlistParserMock;


        [SetUp]
        public void Initialize()
        {
            _menuVMMock = new Mock<IMenuVM>();
            _questionVMMock = new Mock<IQuestionVM>();
            _summaryVMMock = new Mock<ISummaryVM>();
            _textFileReaderMock = new Mock<ITextFileReader>();
            _wordlistParserMock = new Mock<IWordlistParser>();

            _objectUnderTest = new MainWindowVM(_menuVMMock.Object, _questionVMMock.Object, _summaryVMMock.Object,
                                                _textFileReaderMock.Object, _wordlistParserMock.Object);
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
                                  new QuestionsFinishedEventArgs(new QuestionResult(1, 2, TimeSpan.FromSeconds(1))));

            Assert.AreEqual(_summaryVMMock.Object, _objectUnderTest.Content);
        }

        [Test]
        public void ShouldInitializeSummaryViewModelWhenQuestionsAreFinished()
        {
            var questionResult = new QuestionResult(1, 2, TimeSpan.FromSeconds(1));
            _questionVMMock.Raise(p => p.QuestionsFinished += null,
                                  new QuestionsFinishedEventArgs(questionResult));

            _summaryVMMock.Verify(p => p.Initialize(questionResult), Times.Once());
        }

        [Test]
        public void ShouldReadFileWhenFileImportIsRequested()
        {
            _menuVMMock.Raise(p => p.FileImportRequested += null, new FileImportRequestedEventArgs("somefile.csv"));

            _textFileReaderMock.Verify(p => p.Read("somefile.csv"), Times.Once());
        }

        [Test]
        public void ShouldParseLinesWhenFileImportIsRequested()
        {
            var lines = new List<string>();
            _textFileReaderMock.Setup(p => p.Read("somefile.csv")).Returns(lines);
            _menuVMMock.Raise(p => p.FileImportRequested += null, new FileImportRequestedEventArgs("somefile.csv"));

            _wordlistParserMock.Verify(p => p.Import(lines), Times.Once());
        }

        [Test]
        public void ShouldSetContentToQuestionVMWhenFileImportIsRequested()
        {
            _menuVMMock.Raise(p => p.FileImportRequested += null, new FileImportRequestedEventArgs("somefile.csv"));

            Assert.AreEqual(_objectUnderTest.Content, _questionVMMock.Object);
        }
    }
}
