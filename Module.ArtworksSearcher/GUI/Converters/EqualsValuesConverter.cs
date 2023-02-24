using System.Globalization;
using System.Windows.Data;

namespace Module.ArtworksSearcher.GUI.Converters;

public class EqualsValuesConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length <= 1)
            return true;
        if (values[0] is null)
            return false;

        for (var i = 1; i < values.Length; i++)
        {
            if (values[i] is null)
                return false;
            if (!values[i].Equals(values[0]))
                return false;
        }
        return true;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}