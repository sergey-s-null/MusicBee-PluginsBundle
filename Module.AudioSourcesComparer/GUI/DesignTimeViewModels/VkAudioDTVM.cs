using System.Windows.Input;
using Module.AudioSourcesComparer.GUI.AbstractViewModels;
using Module.Mvvm.Extension;

#pragma warning disable CS0067

namespace Module.AudioSourcesComparer.GUI.DesignTimeViewModels;

public sealed class VkAudioDTVM : IVkAudioVM
{
    public event EventHandler<EventArgs>? DeleteRequested;

    public long Id { get; }
    public string Artist { get; }
    public string Title { get; }

    public ICommand SetArtistAndTitleToClipboardCmd => new RelayCommand(_ => { });
    public ICommand DeleteCmd => new RelayCommand(_ => { });

    public VkAudioDTVM()
    {
        Id = 456240678;
        Artist = "Some Artist";
        Title = "Some Song";
    }

    public VkAudioDTVM(long id, string artist, string title)
    {
        Id = id;
        Artist = artist;
        Title = title;
    }
}