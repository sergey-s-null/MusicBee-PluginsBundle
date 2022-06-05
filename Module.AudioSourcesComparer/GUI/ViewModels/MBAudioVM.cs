using System.IO;
using System.Windows;
using System.Windows.Input;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Root.MVVM;

namespace Module.AudioSourcesComparer.GUI.ViewModels
{
    public class MBAudioVM : IMBAudioVM
    {
        private readonly string _filePath;
        public long VkId { get; }
        public int Index { get; }
        public string Artist { get; }
        public string Title { get; }

        public ICommand SetFilePathToClipboardCmd =>
            _setFilePathToClipboardCmd ??= new RelayCommand(_ => SetFilePathToClipboard());

        public ICommand SetFileNameToClipboardCmd =>
            _setFileNameToClipboardCmd ??= new RelayCommand(_ => SetFileNameToClipboard());

        public ICommand SetArtistAndTitleToClipboardCmd =>
            _setArtistAndTitleToClipboardCmd ??= new RelayCommand(_ => SetArtistAndTitleToClipboard());

        private ICommand? _setFilePathToClipboardCmd;
        private ICommand? _setFileNameToClipboardCmd;
        private ICommand? _setArtistAndTitleToClipboardCmd;

        public MBAudioVM(string filePath, long vkId, int index, string artist, string title)
        {
            _filePath = filePath;
            VkId = vkId;
            Index = index;
            Artist = artist;
            Title = title;
        }

        private void SetFilePathToClipboard()
        {
            Clipboard.SetText(_filePath);
        }

        private void SetFileNameToClipboard()
        {
            Clipboard.SetText(Path.GetFileName(_filePath));
        }

        private void SetArtistAndTitleToClipboard()
        {
            Clipboard.SetText($"{Artist} - {Title}");
        }
    }
}