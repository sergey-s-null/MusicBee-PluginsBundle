using PropertyChanged;

namespace Module.PlaylistsExporter.GUI.Settings
{
    [AddINotifyPropertyChangedInterface]
    public class PlaylistVM
    {
        public bool Selected { get; set; }
        public string RelativePath { get; set; } = "";
    }
}