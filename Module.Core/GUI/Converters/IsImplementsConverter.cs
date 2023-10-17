using System.Globalization;
using System.Windows.Data;

namespace Module.Core.GUI.Converters;

public sealed class IsImplementsConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return false;
        }

        if (parameter is not Type baseType)
        {
            throw new ArgumentException("Parameter is not type.", nameof(parameter));
        }

        return baseType.IsInstanceOfType(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}