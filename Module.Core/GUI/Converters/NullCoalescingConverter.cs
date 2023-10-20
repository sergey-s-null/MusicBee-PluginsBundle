using System.Globalization;
using System.Windows.Data;

namespace Module.Core.GUI.Converters;

public sealed class NullCoalescingConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return value ?? parameter;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}