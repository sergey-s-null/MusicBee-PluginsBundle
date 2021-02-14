using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBeePlugin_VkMusicDownloader
{
    class VkAudioVM : BaseViewModel
    {
        #region Bindings

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                NotifyPropChanged(nameof(IsSelected));
            }
        }

        private string _id = "";
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropChanged(nameof(Id));
            }
        }

        private string _artist = "";
        public string Artist
        {
            get => _artist;
            set
            {
                _artist = value;
                NotifyPropChanged(nameof(Artist));
            }
        }

        private string _title = "";
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropChanged(nameof(Title));
            }
        }

        private int _duration = 0;
        public int Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                NotifyPropChanged(nameof(Duration), nameof(DurationString));
            }
        }
        public string DurationString
            => $"{_duration / 60}:" + (_duration % 60).ToString().PadLeft(2, '0');

        #endregion

        public readonly string Url;

        public VkAudioVM(string url)
        {
            Url = url;
        }

    }
}
