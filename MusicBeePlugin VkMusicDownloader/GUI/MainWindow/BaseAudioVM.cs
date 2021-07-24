using System;

namespace VkMusicDownloader.GUI.MainWindow
{
    public abstract class BaseAudioVM : BaseViewModel, IComparable
    {

        #region Bindings

        private long _vkId = -1;
        public long VkId
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

        #region IComparable

        public abstract int CompareTo(object obj);

        #endregion

    }
}
