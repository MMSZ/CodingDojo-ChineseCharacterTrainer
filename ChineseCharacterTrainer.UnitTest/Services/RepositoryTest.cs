using ChineseCharacterTrainer.Implementation.ServiceReference;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.UnitTest.Services
{
    public class RepositoryTest
    {
        private IRepository _objectUnderTest;
        private Mock<IChineseCharacterTrainerService> _serviceMock;

        [SetUp]
        public void Initialize()
        {
            _serviceMock = new Mock<IChineseCharacterTrainerService>();

            _objectUnderTest = new Repository(_serviceMock.Object);
        }

        [Test]
        public void ShouldAddItemToServiceWhenAddingDictionary()
        {
            var dictionary = new Dictionary("MyDict", null);

            _objectUnderTest.AddDictionary(dictionary);

            _serviceMock.Verify(p => p.AddDictionary(dictionary));
        }

        [Test]
        public void ShouldGetDictionary()
        {
            _serviceMock.Setup(p => p.GetDictionaries()).Returns(new List<Dictionary>
                {
                    new Dictionary("1", null),
                    new Dictionary("2", null)
                });

            var dictionaries = _objectUnderTest.GetDictionaries();

            Assert.AreEqual(2, dictionaries.Count);
        }

        [Test]
        public void ShouldAddHighscoreToServiceWhenAddingHighscore()
        {
            var highscore = new Highscore(null, null, 0);

            _objectUnderTest.AddHighscore(highscore);

            _serviceMock.Verify(p => p.AddHighscore(highscore));
        }

        [Test]
        public void ShouldGetHighscore()
        {
            _serviceMock.Setup(p => p.GetHighscores()).Returns(new List<Highscore>
                {
                    new Highscore(null, null, 0),
                    new Highscore(null, null, 0)
                });

            var highscores = _objectUnderTest.GetHighscores();

            Assert.AreEqual(2, highscores.Count);
        }
    }
}
