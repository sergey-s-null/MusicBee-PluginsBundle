using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VkMusicDownloader.GUI.Converters
{
    class TrueToHiddenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool bValue)
            {
                if (bValue)
                    return Visibility.Hidden;
                else
                    return Visibility.Visible;
            }
            else
                throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
