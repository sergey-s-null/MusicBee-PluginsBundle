using System.ComponentModel;
using System.Windows.Input;
using Module.ArtworksSearcher.Settings;

namespace Module.ArtworksSearcher.GUI.Settings
{
    public interface IArtworksSearcherSettingsVM : IArtworksSearcherSettings
    {
        ICommand ChangeOsuSongsDirCmd { get; }
    }
}