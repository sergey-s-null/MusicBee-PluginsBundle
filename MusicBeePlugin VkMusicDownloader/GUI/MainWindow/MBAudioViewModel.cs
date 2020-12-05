using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBeePlugin_VkMusicDownloader
{
    class MBAudioViewModel : BaseViewModel
    {
        private int _index1 = 0;
        public int Index1
        {
            get => _index1;
            set
            {
                _index1 = value;
                NotifyPropChanged(nameof(Index1), nameof(Index1Str));
            }
        }
        public string Index1Str => _index1.ToString().PadLeft(2, '0');

        private int _index2 = 0;
        public int Index2
        {
            get => _index2;
            set
            {
                _index2 = value;
                NotifyPropChanged(nameof(Index2), nameof(Index2Str));
            }
        }
        public string Index2Str => _index2.ToString().PadLeft(2, '0');

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

    }
}
