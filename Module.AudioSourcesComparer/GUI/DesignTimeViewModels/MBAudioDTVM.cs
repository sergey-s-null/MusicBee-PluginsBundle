using System.Windows.Input;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Module.Mvvm.Extension;

namespace Module.AudioSourcesComparer.GUI.DesignTimeViewModels
{
    public sealed class MBAudioDTVM : IMBAudioVM
    {
        public long VkId { get; }
        public int Index { get; }
        public string Artist { get; }
        public string Title { get; }

        public ICommand SetFilePathToClipboardCmd { get; } = new RelayCommand(_ => { });
        public ICommand SetFileNameToClipboardCmd { get; } = new RelayCommand(_ => { });
        public ICommand SetArtistAndTitleToClipboardCmd { get; } = new RelayCommand(_ => { });

        public MBAudioDTVM()
        {
            VkId = 456240678;
            Index = 1002;
            Artist = "Default Artist";
            Title = "Default Title";
        }

        public MBAudioDTVM(long vkId, int index, string artist, string title)
        {
            VkId = vkId;
            Index = index;
            Artist = artist;
            Title = title;
        }
    }
}