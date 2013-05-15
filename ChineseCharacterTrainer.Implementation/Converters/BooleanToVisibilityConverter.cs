using System.Windows;

namespace ChineseCharacterTrainer.Implementation.Converters
{
    public class BooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        protected override Visibility PositiveResult
        {
            get { return Visibility.Visible; }
        }

        protected override Visibility NegativeResult
        {
            get { return Visibility.Collapsed; }
        }
    }
}
