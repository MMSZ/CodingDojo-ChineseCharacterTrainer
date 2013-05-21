using System;
using System.Globalization;
using System.Windows.Data;
using ChineseCharacterTrainer.Implementation.Services;

namespace ChineseCharacterTrainer.Implementation.Converters
{
    public class PinyinConverter : IValueConverter
    {
        private IPinyinBeautifier _beautifier;

        public IPinyinBeautifier Beautifier
        {
            get { return _beautifier ?? (new PinyinBeautifier()); }
            set { _beautifier = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;
            return Beautifier.Beautify(stringValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}