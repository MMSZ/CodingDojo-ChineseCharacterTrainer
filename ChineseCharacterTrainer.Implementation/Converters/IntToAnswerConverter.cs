using System;
using System.Globalization;
using System.Windows.Data;

namespace ChineseCharacterTrainer.Implementation.Converters
{
    public class IntToAnswerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var intValue = (int) value;
                if (intValue < 0) return null;
                
                var boolParameter = ConvertObjectToBool(parameter);

                if (boolParameter)
                {
                    return intValue == 1 ? string.Format("1 correct answer") : string.Format("{0} correct answers", value);
                }
                
                return intValue == 1 ? string.Format("1 incorrect answer") : string.Format("{0} incorrect answers", value);
            }

            return null;
        }

        private static bool ConvertObjectToBool(object value)
        {
            if ((value is bool) || (value is string))
            {
                return System.Convert.ToBoolean(value);
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
