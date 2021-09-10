using System;
using System.Globalization;
using System.Windows.Data;

namespace Module.VkAudioDownloader.GUI.Converters
{
    class EqualsTypesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return false;
            if (parameter is not Type)
                return false;
            return value.GetType().Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
