using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace SafeSpace.Converters
{
    public class BoolToJoinTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool isJoined && isJoined ? "Joined" : "Join Meetup";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == "Joined";
        }
    }
}
