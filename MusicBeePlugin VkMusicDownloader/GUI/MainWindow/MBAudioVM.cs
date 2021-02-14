using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBeePlugin_VkMusicDownloader
{
    class MBAudioVM : BaseViewModel
    {
        #region Bindings

        private int _index = -1;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                NotifyPropChanged(nameof(Index), nameof(Index1Str), nameof(Index2Str));
            }
        }

        public string Index1Str => (Index / 20 + 1).ToString().PadLeft(2, '0');
        public string Index2Str => (Index % 20 + 1).ToString().PadLeft(2, '0');

        private string _vkId = "";
        public string VkId
        {
            get => _vkId;
            set
            {
                _vkId = value;
                NotifyPropChanged(nameof(VkId));
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

        #endregion
    }
}
