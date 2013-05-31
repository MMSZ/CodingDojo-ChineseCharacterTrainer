using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Implementation.ViewModels;
using ChineseCharacterTrainer.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.UnitTest.ViewModels
{
    class HighscoreVMTest
    {
        private IHighscoreVM _objectUnderTest;

        private Mock<IRepository> _repositoryMock;

        private readonly Dictionary _testDictionary = new Dictionary("TestDict1", null);

        private Highscore _currentHighscore;

        private readonly User _userFrank = new User("Frank");
        private readonly User _userSandra = new User("Sandra");
        private readonly User _userMyself = new User("Myself");
        
        [SetUp]
        public void Initialize()
        {
            _currentHighscore = CreateCurrentHighscore();

            var highscores = new List<Highscore>
                {
                    CreateHighscore(_userFrank, _testDictionary, 80),
                    CreateHighscore(_userMyself, _testDictionary, 50),
                    CreateHighscore(_userSandra, _testDictionary, 40),
                    CreateHighscore(_userSandra, _testDictionary, 70),
                    CreateHighscore(_userMyself, new Dictionary("dict2", null), 20),
                    _currentHighscore,
                    CreateHighscore(_userFrank, _testDictionary, 30),
                    CreateHighscore(_userFrank, _testDictionary, 60)
                };

            _repositoryMock = new Mock<IRepository>();
            _repositoryMock.Setup(p => p.GetAll<Highscore>()).Returns(highscores);

            _objectUnderTest = new HighscoreVM(_repositoryMock.Object);
        }

        [Test]
        public void ShouldRaiseEventWhenContinueCommandIsExecuted()
        {
            var eventWasRaised = false;
            _objectUnderTest.ReturnToMenuRequested += () => eventWasRaised = true;

            _objectUnderTest.ContinueCommand.Execute(null);

            Assert.IsTrue(eventWasRaised);
        }

        [Test]
        public void ShouldSetCurrentHighscoreWhenInitializing()
        {
            _objectUnderTest.Initialize(_currentHighscore);

            Assert.AreEqual(_currentHighscore, _objectUnderTest.CurrentHighscore);
        }

        [Test]
        public void ShouldGetBestHighscoreWhenInitializing()
        {
            _objectUnderTest.Initialize(_currentHighscore);

            Assert.AreEqual(5, _objectUnderTest.Highscores.Count);
            Assert.AreEqual(10, _objectUnderTest.Highscores[0].Score);
            Assert.AreEqual(30, _objectUnderTest.Highscores[1].Score);
            Assert.AreEqual(40, _objectUnderTest.Highscores[2].Score);
            Assert.AreEqual(50, _objectUnderTest.Highscores[3].Score);
            Assert.AreEqual(60, _objectUnderTest.Highscores[4].Score);
        }

        [Test]
        public void ShouldGetBestUserHighscoreWhenInitializing()
        {
            _objectUnderTest.Initialize(_currentHighscore);

            Assert.AreEqual(_currentHighscore, _objectUnderTest.BestHighscore);
        }

        private Highscore CreateCurrentHighscore()
        {
            return CreateHighscore(_userMyself, _testDictionary, 10);
        }

        private static Highscore CreateHighscore(User user, Dictionary dictionary, int score)
        {
            return new Highscore(user, dictionary, score);
        }
    }
}
