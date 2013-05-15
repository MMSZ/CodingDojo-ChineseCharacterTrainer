using System;
using System.Globalization;
using System.Windows.Data;

namespace ChineseCharacterTrainer.Implementation.Converters
{
    public abstract class BooleanConverter<T> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = GetBooleanValueFromObject(value, false);
            var boolParameter = GetBooleanValueFromObject(parameter, true);

            var result = boolValue == boolParameter ? PositiveResult : NegativeResult;
            return result;
        }

        private static bool GetBooleanValueFromObject(object value, bool defaultValue)
        {
            if (value == null)
            {
                return defaultValue;
            }

            bool boolValue;

            if (value is bool)
            {
                boolValue = (bool)value;
            }
            else if (value is string)
            {
                if (!Boolean.TryParse(value as string, out boolValue))
                {
                    boolValue = defaultValue;
                }
            }
            else
            {
                boolValue = false;
            }

            return boolValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }

        protected abstract T PositiveResult { get; }
        protected abstract T NegativeResult { get; }
    }
}
