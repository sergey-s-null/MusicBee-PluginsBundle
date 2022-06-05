using System.Windows.Input;

namespace Module.AudioSourcesComparer.GUI.AbstractViewModels
{
    public interface IVkAudioVM
    {
        long Id { get; }
        string Artist { get; }
        string Title { get; }

        ICommand SetArtistAndTitleToClipboardCmd { get; }
        ICommand DeleteCmd { get; }
    }
}