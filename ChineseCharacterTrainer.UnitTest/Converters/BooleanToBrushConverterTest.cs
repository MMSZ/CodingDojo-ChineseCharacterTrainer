using System.Windows.Data;
using System.Windows.Media;
using ChineseCharacterTrainer.Implementation.Converters;

namespace ChineseCharacterTrainer.UnitTest.Converters
{
    public class BooleanToBrushConverterTest : BooleanConverterTest<SolidColorBrush>
    {
        protected override IValueConverter CreateObjectUnderTest()
        {
            return new BooleanToBrushConverter();
        }

        protected override bool ValuesAreEqual(SolidColorBrush expected, SolidColorBrush actual)
        {
            return expected.Color == actual.Color;
        }

        protected override SolidColorBrush ExpectedPositiveValue { get { return new SolidColorBrush(Colors.Green);} }

        protected override SolidColorBrush ExpectedNegativeValue { get { return new SolidColorBrush(Colors.Red); } }
    }
}
