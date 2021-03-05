using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkMusicDownloader.GUI
{
    public class MBAudioVM : BaseAudioVM
    {
        #region Bindings

        private int _index = -1;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                NotifyPropChanged(nameof(Index));
            }
        }

        public string Index1Str => (Index / 20 + 1).ToString().PadLeft(2, '0');
        public string Index2Str => (Index % 20 + 1).ToString().PadLeft(2, '0');

        #endregion

        public override int CompareTo(object obj)
        {
            if (this == obj)
                return 0;
            if (obj is MBAudioVM other)
                return Index.CompareTo(other.Index);
            if (obj is VkAudioVM otherVk)
                return -1;
            if (obj is BaseAudioVM)
                throw new NotImplementedException();

            throw new NotSupportedException();
        }
    }
}
