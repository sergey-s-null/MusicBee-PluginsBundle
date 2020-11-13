using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBeePlugin_VkMusicDownloader
{
    class VkAudioViewModel : BaseViewModel
    {
        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                NotifyPropertyChanged(nameof(IsSelected));
            }
        }

        private string _id = "";
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        private string _artist = "";
        public string Artist
        {
            get => _artist;
            set
            {
                _artist = value;
                NotifyPropertyChanged(nameof(Artist));
            }
        }

        private string _title = "";
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        private int _duration = 0;
        public int Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                NotifyPropertiesChanged(nameof(Duration), nameof(DurationString));
            }
        }
        public string DurationString
            => $"{_duration / 60}:" + (_duration % 60).ToString().PadLeft(2, '0');

        public readonly string Url;

        public VkAudioViewModel(string url)
        {
            Url = url;
        }

    }
}
