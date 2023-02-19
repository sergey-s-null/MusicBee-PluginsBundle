using System.Globalization;
using System.Windows.Data;

namespace Module.Core.GUI.Converters
{
    public sealed class GroupConverter : List<IValueConverter>, IValueConverter
    {
        public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}