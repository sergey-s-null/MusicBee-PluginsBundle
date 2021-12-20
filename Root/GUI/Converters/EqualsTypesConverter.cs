using System;
using System.Globalization;
using System.Windows.Data;

namespace Root.GUI.Converters
{
    public class EqualsTypesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter is Type && value.GetType().Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}