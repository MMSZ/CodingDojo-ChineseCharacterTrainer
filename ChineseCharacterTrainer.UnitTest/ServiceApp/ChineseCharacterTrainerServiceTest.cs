using System.Collections.Generic;
using ChineseCharacterTrainer.Model;
using ChineseCharacterTrainer.ServiceApp;
using ChineseCharacterTrainer.ServiceApp.Persistence;
using Moq;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.ServiceApp
{
    class ChineseCharacterTrainerServiceTest
    {
        private ChineseCharacterTrainerService _objectUnderTest;
        private Mock<IChineseTrainerContext> _chineseTrainerContextMock;
        
        [SetUp]
        public void Initialize()
        {
            _chineseTrainerContextMock = new Mock<IChineseTrainerContext>();
            _objectUnderTest = new ChineseCharacterTrainerService();
            _objectUnderTest.ChineseTrainerContext = _chineseTrainerContextMock.Object;
        }

        [Test]
        public void ShouldAddDictionaryToContextWhenAdding()
        {
            var dictionary = CreateTestDictionaryWithEntries();
            _objectUnderTest.AddDictionary(dictionary);

            _chineseTrainerContextMock.Verify(p => p.Add(dictionary));
        }

        [Test]
        public void ShouldSaveChangesWhenAdding()
        {
            var dictionary = CreateTestDictionaryWithEntries();
            _objectUnderTest.AddDictionary(dictionary);

            _chineseTrainerContextMock.Verify(p => p.SaveChanges());
        }

        [Test]
        public void ShouldReturnDictionariesFromContext()
        {
            _chineseTrainerContextMock.Setup(p => p.GetAll<Dictionary>()).Returns(
                new List<Dictionary> {CreateTestDictionaryWithEntries()});

            var dictionaries = _objectUnderTest.GetDictionaries();

            Assert.AreEqual(1, dictionaries.Count);
        }

        [Test]
        public void ShouldReturnDefaultIfContextIsNotInitialize()
        {
            _objectUnderTest = new ChineseCharacterTrainerService();
            var context = _objectUnderTest.ChineseTrainerContext;

            Assert.IsInstanceOf<ChineseTrainerContext>(context);
        }

        private Dictionary CreateTestDictionaryWithEntries()
        {
            var entries = new List<DictionaryEntry> { new DictionaryEntry("你", "ni3", null) };
            var dictionary = new Dictionary("Test", entries);
            return dictionary;
        }
    }
}
