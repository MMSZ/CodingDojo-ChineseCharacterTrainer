using ChineseCharacterTrainer.Implementation.Converters;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.Converters
{
    public class IntToAnswerConverterTest
    {
        private IntToAnswerConverter _objectUnderTest;

        [SetUp]
        public void Initialize()
        {
            _objectUnderTest = new IntToAnswerConverter();
        }

        [TestCase(0, true, "0 correct answers")]
        [TestCase(1, true, "1 correct answer")]
        [TestCase(100, true, "100 correct answers")]
        [TestCase(-1, true, null)]
        [TestCase(0, false, "0 incorrect answers")]
        [TestCase(1, false, "1 incorrect answer")]
        [TestCase(100, false, "100 incorrect answers")]
        [TestCase(-1, false, null)]
        [TestCase(1, null, "1 correct answer")]
        [TestCase(1, "true", "1 correct answer")]
        [TestCase(1, "false", "1 incorrect answer")]
        [TestCase(null, null, null)]
        public void ShouldConvertCorrectValues(object value, object parameter, string expected)
        {
            var actual = _objectUnderTest.Convert(value, null, parameter, null);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldConvertBackReturnsNull()
        {
            var actual = _objectUnderTest.ConvertBack(null, null, null, null);

            Assert.IsNull(actual);
        }
    }
}
