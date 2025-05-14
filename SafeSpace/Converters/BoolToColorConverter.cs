using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace SafeSpace.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public Color IncomingColor { get; set; } = Colors.White;
        public Color OutgoingColor { get; set; } = Colors.LightGreen;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isIncoming)
            {
                return isIncoming ? IncomingColor : OutgoingColor;
            }
            return IncomingColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
