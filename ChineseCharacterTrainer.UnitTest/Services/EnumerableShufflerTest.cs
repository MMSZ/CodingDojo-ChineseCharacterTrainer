using ChineseCharacterTrainer.Implementation.Services;
using ChineseCharacterTrainer.Implementation.Utilities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChineseCharacterTrainer.UnitTest.Services
{
    class EnumerableShufflerTest
    {
        private IEnumerableShuffler _objectUnderTest;

        private Mock<IDateTime> _dateTimeMock;

        private List<int> _testEnumerable;

        [SetUp]
        public void Initialize()
        {
            _testEnumerable = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            _dateTimeMock = new Mock<IDateTime>();
            _dateTimeMock.Setup(p => p.Now).Returns(new DateTime(2000, 1, 1));
            _objectUnderTest = new EnumerableShuffler(_dateTimeMock.Object);
        }

        [Test]
        public void ShouldShuffleEnumerable()
        {
            var shuffledEnumerable = _objectUnderTest.Shuffle(_testEnumerable).ToList();

            Assert.AreEqual(10, shuffledEnumerable.Union(_testEnumerable).Count());
            Assert.AreEqual(9, shuffledEnumerable[0]);
            Assert.AreEqual(4, shuffledEnumerable[1]);
            Assert.AreEqual(2, shuffledEnumerable[2]);
            Assert.AreEqual(8, shuffledEnumerable[3]);
            Assert.AreEqual(3, shuffledEnumerable[4]);
            Assert.AreEqual(0, shuffledEnumerable[5]);
            Assert.AreEqual(7, shuffledEnumerable[6]);
            Assert.AreEqual(5, shuffledEnumerable[7]);
            Assert.AreEqual(6, shuffledEnumerable[8]);
            Assert.AreEqual(1, shuffledEnumerable[9]);
        }

        [Test]
        public void ShouldReturnEmptyEnumerableForNullArgument()
        {
            var result = _objectUnderTest.Shuffle<int>(null);

            Assert.AreEqual(0, result.Count());
        }
    }
}
