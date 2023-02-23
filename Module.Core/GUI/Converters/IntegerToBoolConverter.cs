using System.Globalization;
using System.Windows.Data;
using Module.Core.Extensions;
using Module.Core.GUI.Converters.Enums;

namespace Module.Core.GUI.Converters;

public sealed class IntegerToBoolConverter : IValueConverter
{
    public CompareOperator CompareOperator { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int valueInt && parameter is int parameterInt)
        {
            return CompareOperator.Compare(valueInt, parameterInt);
        }

        throw new NotSupportedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}