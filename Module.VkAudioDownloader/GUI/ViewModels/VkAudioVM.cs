using Module.VkAudioDownloader.GUI.AbstractViewModels;
using PropertyChanged;

namespace Module.VkAudioDownloader.GUI.ViewModels;

[AddINotifyPropertyChangedInterface]
public sealed class VkAudioVM : IVkAudioVM
{
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (value && IsInIncoming)
            {
                throw new InvalidOperationException("Could not select audio that contains in Incoming.");
            }

            _isSelected = value;
        }
    }

    public long VkId { get; }
    public string Artist { get; }
    public string Title { get; }
    public IVkAudioUrlVM? Url { get; }
    public bool IsInIncoming { get; }
    public IReadOnlyList<string> Warnings { get; }

    private bool _isSelected;

    public VkAudioVM(
        long vkId,
        string artist,
        string title,
        IVkAudioUrlVM? url,
        bool isInIncoming)
    {
        VkId = vkId;
        Artist = artist;
        Title = title;
        Url = url;
        IsInIncoming = isInIncoming;

        Warnings = GetWarnings();
    }

    private IReadOnlyList<string> GetWarnings()
    {
        var warnings = new List<string>();

        if (Url is null)
        {
            warnings.Add("No download url.");
        }

        if (IsInIncoming)
        {
            warnings.Add("Audio already in Incoming.");
        }

        return warnings;
    }
}