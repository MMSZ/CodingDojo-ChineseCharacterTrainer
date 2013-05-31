using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Implementation.ViewModels;
using ChineseCharacterTrainer.Model;
using Moq;
using NUnit.Framework;
using System;

namespace ChineseCharacterTrainer.UnitTest.ViewModels
{
    public class SummaryVMTest
    {
        private ISummaryVM _objectUnderTest;

        private readonly QuestionResult _questionResult = new QuestionResult(1, 2, TimeSpan.FromSeconds(1), 100);
        private readonly Dictionary _dictionary = new Dictionary("Test", null);

        private Mock<IRepository> _repositoryMock;

        [SetUp]
        public void Initialize()
        {
            _repositoryMock = new Mock<IRepository>();

            _objectUnderTest = new SummaryVM(_repositoryMock.Object);
            _objectUnderTest.Initialize(_dictionary, _questionResult);
        }

        [Test]
        public void ShouldGetCorrectNumberOfCorrectAnswersAfterInitializing()
        {
            Assert.AreEqual(1, _objectUnderTest.NumberOfCorrectAnswers);
        }

        [Test]
        public void ShouldGetCorrectNumberOfIncorrectAnswersAfterInitializing()
        {
            Assert.AreEqual(2, _objectUnderTest.NumberOfIncorrectAnswers);
        }

        [Test]
        public void ShouldGetCorrectDurationAfterInitializing()
        {
            Assert.AreEqual(TimeSpan.FromSeconds(1), _objectUnderTest.Duration);
        }

        [Test]
        public void ShouldGetCorrectScoreAfterInitializing()
        {
            Assert.AreEqual(100, _objectUnderTest.Score);
        }

        [Test]
        public void CanUploadHighscoreWhenUsernameIsEntered()
        {
            _objectUnderTest.Username = "Frank";

            var canExecute = _objectUnderTest.UploadScoreCommand.CanExecute(null);

            Assert.IsTrue(canExecute);
        }

        [Test]
        public void CannotUploadHighscoreWhenUsernameIsEmpty()
        {
            _objectUnderTest.Username = null;

            var canExecute = _objectUnderTest.UploadScoreCommand.CanExecute(null);

            Assert.IsFalse(canExecute);
        }

        [Test]
        public void ShouldUploadHighscore()
        {
            _objectUnderTest.Username = "Frank";

            _objectUnderTest.UploadScoreCommand.Execute(null);

            _repositoryMock.Verify(p=>p.Add(It.IsAny<Highscore>()));
        }

        [Test]
        public void ShouldRaiseEventAfterUploadHighscore()
        {
            Highscore score = null;
            _objectUnderTest.UploadFinished += highscore => score = highscore;
            _objectUnderTest.Username = "Frank";
            
            _objectUnderTest.UploadScoreCommand.Execute(null);

            Assert.IsNotNull(score);
        }
    }
}
