using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Module.Core.GUI.Converters;

public sealed class BoolToVisibilityConverter : IValueConverter
{
    public Visibility VisibilityOnFalse { get; set; }
    public Visibility VisibilityOnTrue { get; set; }
        
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue
                ? VisibilityOnTrue
                : VisibilityOnFalse;
        }
            
        throw new NotSupportedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}