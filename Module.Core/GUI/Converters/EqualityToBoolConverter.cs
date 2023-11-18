using System.Globalization;
using System.Windows.Data;

namespace Module.Core.GUI.Converters;

public sealed class EqualityToBoolConverter : IValueConverter
{
    public bool ValueOnEqual { get; set; }

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null || parameter is null)
        {
            return !ValueOnEqual;
        }

        return value.Equals(parameter)
            ? ValueOnEqual
            : !ValueOnEqual;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}