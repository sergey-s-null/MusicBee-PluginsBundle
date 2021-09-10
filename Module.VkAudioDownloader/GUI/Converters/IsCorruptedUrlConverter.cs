using System;
using System.Globalization;
using System.Windows.Data;
using Module.VkAudioDownloader.GUI.VkAudioDownloaderWindow;

namespace Module.VkAudioDownloader.GUI.Converters
{
    public class IsCorruptedUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BaseAudioVM baseAudioVM)
            {
                if (baseAudioVM is VkAudioVM vkAudioVM)
                    return vkAudioVM.IsCorraptedUrl;
                else
                    return false;
            }
            else
                throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
