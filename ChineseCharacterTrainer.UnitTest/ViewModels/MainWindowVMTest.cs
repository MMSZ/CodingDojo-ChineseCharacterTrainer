using System;
using System.Collections.Generic;
using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.Utilities;
using ChineseCharacterTrainer.Implementation.ViewModels;
using Moq;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.ViewModels
{
    public class MainWindowVMTest
    {
        private IMainWindowVM _objectUnderTest;
        private Mock<IServiceLocator> _serviceLocatorMock;
        private Mock<IQuestionVM> _questionVMMock;
        private Mock<ISummaryVM> _summaryVMMock;

        [SetUp]
        public void Initialize()
        {
            _serviceLocatorMock = new Mock<IServiceLocator>();
            _questionVMMock = new Mock<IQuestionVM>();
            _summaryVMMock = new Mock<ISummaryVM>();

            _serviceLocatorMock.Setup(p => p.Get<IQuestionVM>()).Returns(_questionVMMock.Object);
            
            _objectUnderTest = new MainWindowVM(_serviceLocatorMock.Object);
        }

        [Test]
        public void ShouldSetContentToQuestionViewModelInConstructor()
        {
            Assert.AreEqual(_questionVMMock.Object, _objectUnderTest.Content);
        }

        [Test]
        public void ShouldInitializeQuestionViewModelInConstructor()
        {
            _questionVMMock.Verify(p=>p.Initialize(It.IsAny<List<DictionaryEntry>>()));
        }

        [Test]
        public void ShouldSetContentToSummaryViewModelWhenQuestionsAreFinished()
        {
            _serviceLocatorMock.Setup(p => p.Get<ISummaryVM>()).Returns(_summaryVMMock.Object);
            _questionVMMock.Raise(p => p.QuestionsFinished += null,
                                  new QuestionsFinishedEventArgs(new QuestionResult(1, 2, TimeSpan.FromSeconds(1))));

            Assert.AreEqual(_summaryVMMock.Object, _objectUnderTest.Content);
        }

        [Test]
        public void ShouldInitializeSummaryViewModelWhenQuestionsAreFinished()
        {
            _serviceLocatorMock.Setup(p => p.Get<ISummaryVM>()).Returns(_summaryVMMock.Object);
            var questionResult = new QuestionResult(1, 2, TimeSpan.FromSeconds(1));
            _questionVMMock.Raise(p => p.QuestionsFinished += null,
                                  new QuestionsFinishedEventArgs(questionResult));

            _summaryVMMock.Verify(p => p.Initialize(questionResult), Times.Once());
        }
    }
}
