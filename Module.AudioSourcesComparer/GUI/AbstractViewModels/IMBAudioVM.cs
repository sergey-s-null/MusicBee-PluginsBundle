using System.Windows.Input;

namespace Module.AudioSourcesComparer.GUI.AbstractViewModels
{
    public interface IMBAudioVM
    {
        long VkId { get; }
        int Index { get; }
        string Artist { get; }
        string Title { get; }

        ICommand SetFilePathToClipboardCmd { get; }
        ICommand SetFileNameToClipboardCmd { get; }
        ICommand SetArtistAndTitleToClipboardCmd { get; }
    }
}