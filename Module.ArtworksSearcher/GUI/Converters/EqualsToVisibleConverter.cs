using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Module.ArtworksSearcher.GUI.Converters;

public sealed class EqualsToVisibleConverter : IValueConverter, IMultiValueConverter
{
    #region IValueConverter

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not null && value.Equals(parameter))
            return Visibility.Visible;
        else
            return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    #endregion

    #region IMultiValueConverter

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="ArgumentException">values.Length less than 2</exception>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2)
            throw new ArgumentException("Wrong length of values[].");

        if (values[0] is null)
            return parameter;

        for (var i = 1; i < values.Length; i++)
        {
            if (!values[0].Equals(values[i]))
                return parameter;
        }
        return Visibility.Visible;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    #endregion
}