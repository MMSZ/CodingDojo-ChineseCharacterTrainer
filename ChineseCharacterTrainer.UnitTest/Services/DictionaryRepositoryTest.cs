using ChineseCharacterTrainer.Implementation.Model;
using ChineseCharacterTrainer.Implementation.Persistence;
using ChineseCharacterTrainer.Implementation.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.UnitTest.Services
{
    public class DictionaryRepositoryTest
    {
        private IDictionaryRepository _objectUnderTest;
        private Mock<IChineseTrainerContext> _contextMock;

        [SetUp]
        public void Initialize()
        {
            _contextMock = new Mock<IChineseTrainerContext>();

            _objectUnderTest = new DictionaryRepository(_contextMock.Object);
        }

        [Test]
        public void ShouldAddItemToDbSetWhenAddingDictionary()
        {
            var dictionary = new Dictionary("MyDict", null);

            _objectUnderTest.Add(dictionary);

            _contextMock.Verify(p => p.Add(dictionary));
        }

        [Test]
        public void ShouldSaveChangesWhenAddingDictionary()
        {
            var dictionary = new Dictionary("MyDict", null);

            _objectUnderTest.Add(dictionary);

            _contextMock.Verify(p => p.SaveChanges());
        }

        [Test]
        public void ShouldGetDictionary()
        {
            _contextMock.Setup(p => p.GetAll<Dictionary>()).Returns(new List<Dictionary>
                {
                    new Dictionary("1", null),
                    new Dictionary("2", null)
                });

            var dictionaries = _objectUnderTest.GetAll();

            Assert.AreEqual(2, dictionaries.Count);
        }
    }
}
