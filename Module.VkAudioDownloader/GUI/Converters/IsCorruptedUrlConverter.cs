using System.Globalization;
using System.Windows.Data;
using Module.VkAudioDownloader.GUI.AbstractViewModels;

namespace Module.VkAudioDownloader.GUI.Converters
{
    // todo maybe delete?
    public sealed class IsCorruptedUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IVkAudioVM vkAudioVM)
            {
                return vkAudioVM.Url is not null
                       && vkAudioVM.Url.IsCorrupted;
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}