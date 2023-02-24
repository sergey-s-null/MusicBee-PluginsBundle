using PropertyChanged;

namespace Module.PlaylistsExporter.GUI.Settings;

[AddINotifyPropertyChangedInterface]
public sealed class PlaylistVM
{
    public bool Selected { get; set; }
    public string RelativePath { get; }

    public PlaylistVM(string relativePath)
    {
        RelativePath = relativePath;
    }
}