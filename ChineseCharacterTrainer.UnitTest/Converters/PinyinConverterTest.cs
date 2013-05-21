using ChineseCharacterTrainer.Implementation.Converters;
using ChineseCharacterTrainer.Implementation.Services;
using Moq;
using NUnit.Framework;

namespace ChineseCharacterTrainer.UnitTest.Converters
{
    public class PinyinConverterTest
    {
        private PinyinConverter _objectUnderTest;
        private Mock<IPinyinBeautifier> _pinyinBeautifierMock;

        [SetUp]
        public void Initialize()
        {
            _pinyinBeautifierMock = new Mock<IPinyinBeautifier>();

            _objectUnderTest = new PinyinConverter();
            _objectUnderTest.Beautifier = _pinyinBeautifierMock.Object;
        }

        [Test]
        public void ShouldForwardCallsToPinyinBeautifier()
        {
            _objectUnderTest.Convert(null, null, null, null);

            _pinyinBeautifierMock.Verify(p => p.Beautify(null));
        }

        [Test]
        public void ShouldReturnNullForConvertBack()
        {
            var convertedValue = _objectUnderTest.ConvertBack(null, null, null, null);

            Assert.IsNull(convertedValue);
        }

        [Test]
        public void ShouldInitializeBeautifierToDefault()
        {
            _objectUnderTest = new PinyinConverter();

            Assert.IsInstanceOf<PinyinBeautifier>(_objectUnderTest.Beautifier);
        }
    }
}
