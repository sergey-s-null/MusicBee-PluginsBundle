using System;
using System.Globalization;
using System.Windows.Data;

namespace Root.GUI.Converters
{
    public class IsInstanceOfConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            return
                value is not null
                && parameter is Type parameterType
                && parameterType.IsInstanceOfType(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}