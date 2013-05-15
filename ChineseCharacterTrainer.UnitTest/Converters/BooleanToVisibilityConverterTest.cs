using System.Windows;
using System.Windows.Data;
using ChineseCharacterTrainer.Implementation.Converters;

namespace ChineseCharacterTrainer.UnitTest.Converters
{
    public class BooleanToVisibilityConverterTest : BooleanConverterTest<Visibility>
    {
        protected override IValueConverter CreateObjectUnderTest()
        {
            return new BooleanToVisibilityConverter();
        }

        protected override Visibility ExpectedPositiveValue
        {
            get { return Visibility.Visible; }
        }

        protected override Visibility ExpectedNegativeValue
        {
            get { return Visibility.Collapsed; }
        }
    }
}
