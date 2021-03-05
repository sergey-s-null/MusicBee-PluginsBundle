using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkMusicDownloader.GUI
{
    public class VkAudioVM : BaseAudioVM
    {
        #region Bindings

        /// <summary>
        /// Последняя добавленная аудиозапись имеет 0 индекс.
        /// </summary>
        private int _insideIndex = - 1;
        public int InsideIndex
        {
            get => _insideIndex;
            set
            {
                _insideIndex = value;
                NotifyPropChanged(nameof(InsideIndex));
            }
        }

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

        private string _url = "";
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                NotifyPropChanged(nameof(Url));
            }
        }

        #endregion

        public override int CompareTo(object obj)
        {
            if (this == obj)
                return 0;
            if (obj is VkAudioVM other)
                return other.InsideIndex.CompareTo(InsideIndex);
            if (obj is MBAudioVM otherMB)
                return 1;
            if (obj is BaseAudioVM)
                throw new NotImplementedException();

            throw new NotSupportedException();
        }
    }
}
