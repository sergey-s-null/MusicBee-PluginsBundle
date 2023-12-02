using System.Globalization;
using System.Windows.Data;

namespace Module.Core.GUI.Converters;

public sealed class NullabilityToBoolConverter : IValueConverter
{
    public bool ValueOnNotNull { get; set; }

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not null
            ? ValueOnNotNull
            : !ValueOnNotNull;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}