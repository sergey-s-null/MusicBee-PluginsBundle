using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Module.Core.GUI.Converters.Enums;

namespace Module.Core.GUI.Converters;

public sealed class IntegerCompareToVisibilityConverter : IValueConverter
{
    public CompareOperator CompareOperator { get; set; }
    public Visibility VisibilityOnTrue { get; set; }
    public Visibility VisibilityOnFalse { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int valueInt && parameter is int parameterInt)
        {
            return Compare(valueInt, parameterInt, CompareOperator)
                ? VisibilityOnTrue
                : VisibilityOnFalse;
        }

        throw new NotSupportedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    private static bool Compare(int first, int second, CompareOperator compareOperator)
    {
        return compareOperator switch
        {
            CompareOperator.Equal => first == second,
            CompareOperator.NotEqual => first != second,
            CompareOperator.LessThan => first < second,
            CompareOperator.LessThanOrEqual => first <= second,
            CompareOperator.GreaterThan => first > second,
            CompareOperator.GreaterThanOrEqual => first >= second,
            _ => throw new ArgumentOutOfRangeException(nameof(compareOperator), compareOperator, null)
        };
    }
}