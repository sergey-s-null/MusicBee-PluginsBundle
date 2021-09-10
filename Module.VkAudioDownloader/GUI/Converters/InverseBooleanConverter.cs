using System;
using System.Globalization;
using System.Windows.Data;

namespace Module.VkAudioDownloader.GUI.Converters
{

    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return !boolValue;
            else
                throw new InvalidOperationException("value must be boolean");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}
