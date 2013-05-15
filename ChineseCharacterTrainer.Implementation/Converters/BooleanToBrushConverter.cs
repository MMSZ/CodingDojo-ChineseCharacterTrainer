using System.Windows.Media;

namespace ChineseCharacterTrainer.Implementation.Converters
{
    public class BooleanToBrushConverter : BooleanConverter<SolidColorBrush>
    {
        protected override SolidColorBrush PositiveResult
        {
            get { return new SolidColorBrush(Colors.Green); }
        }

        protected override SolidColorBrush NegativeResult
        {
            get { return new SolidColorBrush(Colors.Red); }
        }
    }
}
