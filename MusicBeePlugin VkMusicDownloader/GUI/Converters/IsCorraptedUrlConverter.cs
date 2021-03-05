using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VkMusicDownloader.GUI.Converters
{
    public class IsCorraptedUrlConverter : IValueConverter
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
