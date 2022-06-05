using System.Windows;
using System.Windows.Input;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Root.MVVM;

namespace Module.AudioSourcesComparer.GUI.ViewModels
{
    public class VkAudioVM : IVkAudioVM
    {
        public long Id { get; }
        public string Artist { get; }
        public string Title { get; }

        public ICommand CopyArtistAndTitleCmd => _copyArtistAndTitleCmd ??= new RelayCommand(_ => CopyArtistAndTitle());
        private ICommand? _copyArtistAndTitleCmd;

        public ICommand DeleteCmd => _deleteCmd ??= new RelayCommand(_ => Delete());
        private ICommand? _deleteCmd;

        public VkAudioVM(long id, string artist, string title)
        {
            Id = id;
            Artist = artist;
            Title = title;
        }

        private void CopyArtistAndTitle()
        {
            Clipboard.SetText($"{Artist} - {Title}");
        }

        private void Delete()
        {
            // todo
        }
    }
}