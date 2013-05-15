using System;
using System.Linq.Expressions;
using NUnit.Framework;
using System.Windows.Data;

namespace ChineseCharacterTrainer.UnitTest.Converters
{
    public abstract class BooleanConverterTest<TResult>
    {
        private IValueConverter _objectUnderTest;

        [SetUp]
        public void Initialize()
        {
            _objectUnderTest = CreateObjectUnderTest();
        }

        [TestCase(true, true)]
        [TestCase(true, null)]
        public void ShouldConvertNullableBooleanToExpectedPositiveResult(bool? value, bool? converterParameter)
        {
            var actual = _objectUnderTest.Convert(value, null, converterParameter, null);

            Assert.IsTrue(ValuesAreEqual(ExpectedPositiveValue, (TResult)actual));
        }

        [TestCase(false, true)]
        [TestCase(false, null)]
        [TestCase(null, true)]
        [TestCase(null, null)]
        public void ShouldConvertNullableBooleanToExpectedNegativeResult(bool? value, bool? converterParameter)
        {
            var actual = _objectUnderTest.Convert(value, null, converterParameter, null);

            Assert.IsTrue(ValuesAreEqual(ExpectedNegativeValue, (TResult)actual));
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        [TestCase(null, false)]
        public void ShouldConvertBooleanToExpectedPositiveResult(bool value, bool converterParameter)
        {
            var actual = _objectUnderTest.Convert(value, null, converterParameter, null);

            Assert.IsTrue(ValuesAreEqual(ExpectedPositiveValue, (TResult)actual));
        }

        [TestCase(false, true)]
        [TestCase(true, false)]
        public void ShouldConvertBooleanToExpectedNegativeResult(bool value, bool converterParameter)
        {
            var actual = _objectUnderTest.Convert(value, null, converterParameter, null);

            Assert.IsTrue(ValuesAreEqual(ExpectedNegativeValue, (TResult)actual));
        }

        [TestCase("true", "true")]
        [TestCase("false", "false")]
        [TestCase("true", null)]
        [TestCase("true", "")]
        [TestCase(null, "false")]
        public void ShouldConvertStringToExpectedPositiveResult(string value, string converterParameter)
        {
            var actual = _objectUnderTest.Convert(value, null, converterParameter, null);

            Assert.IsTrue(ValuesAreEqual(ExpectedPositiveValue, (TResult)actual));
        }

        [TestCase("false", "true")]
        [TestCase("true", "false")]
        [TestCase("false", null)]
        [TestCase("false", "")]
        [TestCase(null, "true")]
        public void ShouldConvertStringToExpectedNegativeResult(string value, string converterParameter)
        {
            var actual = _objectUnderTest.Convert(value, null, converterParameter, null);

            Assert.IsTrue(ValuesAreEqual(ExpectedNegativeValue, (TResult)actual));
        }

        [Test]
        public void ShouldReturnNegativeForOtherTypesWithTrueParameter()
        {
            var actual = _objectUnderTest.Convert(0, null, true, null);

            Assert.IsTrue(ValuesAreEqual(ExpectedNegativeValue, (TResult)actual));
        }

        [Test]
        public void ShouldReturnPositiveForOtherTypesWithFalseParameter()
        {
            var actual = _objectUnderTest.Convert(0, null, false, null);

            Assert.IsTrue(ValuesAreEqual(ExpectedPositiveValue, (TResult)actual));
        }

        [Test]
        public void ConvertBackShouldReturnFalse()
        {
            var actual = _objectUnderTest.ConvertBack(null, null, null, null);

            Assert.IsTrue((bool) actual);
        }

        protected abstract IValueConverter CreateObjectUnderTest();

        protected virtual bool ValuesAreEqual(TResult expected, TResult actual)
        {
            return expected.Equals(actual);
        }

        protected abstract TResult ExpectedPositiveValue { get; }
        protected abstract TResult ExpectedNegativeValue { get; }
    }
}
