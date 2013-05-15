using System;
using System.Globalization;
using System.Windows.Data;

namespace ChineseCharacterTrainer.Implementation.Converters
{
    public abstract class BooleanConverter<T> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = value != null && (bool)value;
            var boolParameter = parameter == null || (bool)parameter;

            var result = boolValue == boolParameter ? PositiveResult : NegativeResult;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }

        protected abstract T PositiveResult { get; }
        protected abstract T NegativeResult { get; }
    }
}
