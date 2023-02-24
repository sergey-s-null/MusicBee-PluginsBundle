using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Module.Core.GUI.Converters;

public class TrueToHiddenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue 
                ? Visibility.Hidden 
                : Visibility.Visible;
        }
            
        throw new NotSupportedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}