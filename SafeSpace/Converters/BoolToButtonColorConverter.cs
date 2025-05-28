using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace SafeSpace.Converters
{


    public class BoolToButtonColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool isJoined && isJoined ? Color.FromArgb("#FFFFFF") : Color.FromArgb("#ADD8E6");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Color color && color == Color.FromArgb("#FFFFFF");
        }
    }
}
