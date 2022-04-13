using System;
using System.Globalization;
using System.Windows.Data;
using Module.VkAudioDownloader.GUI.AbstractViewModels;
using Module.VkAudioDownloader.GUI.ViewModels;

namespace Module.VkAudioDownloader.GUI.Converters
{
    public class IsCorruptedUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IAudioVM baseAudioVM)
            {
                return baseAudioVM is VkAudioVM { IsCorruptedUrl: true };
            }
            
            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
