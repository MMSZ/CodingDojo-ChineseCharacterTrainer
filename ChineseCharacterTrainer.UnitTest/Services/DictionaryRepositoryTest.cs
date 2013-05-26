using ChineseCharacterTrainer.Implementation.ServiceReference;
using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChineseCharacterTrainer.UnitTest.Services
{
    public class DictionaryRepositoryTest
    {
        private IDictionaryRepository _objectUnderTest;
        private Mock<IChineseCharacterTrainerService> _serviceMock;

        [SetUp]
        public void Initialize()
        {
            _serviceMock = new Mock<IChineseCharacterTrainerService>();

            _objectUnderTest = new DictionaryRepository(_serviceMock.Object);
        }

        [Test]
        public void ShouldAddItemToDbSetWhenAddingDictionary()
        {
            var dictionary = new Dictionary("MyDict", null);

            _objectUnderTest.Add(dictionary);

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

            var dictionaries = _objectUnderTest.GetAll();

            Assert.AreEqual(2, dictionaries.Count);
        }
    }
}
