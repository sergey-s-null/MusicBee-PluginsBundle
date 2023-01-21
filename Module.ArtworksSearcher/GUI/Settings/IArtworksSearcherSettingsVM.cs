using System.Windows.Input;
using Module.Settings.Gui.AbstractViewModels;

namespace Module.ArtworksSearcher.GUI.Settings
{
    public interface IArtworksSearcherSettingsVM : IBaseSettingsVM
    {
        string GoogleCX { get; set; }
        string GoogleKey { get; set; }
        int ParallelDownloadsCount { get; set; }
        string OsuSongsDir { get; set; }
        long MinOsuImageByteSize { get; set; }

        ICommand ChangeOsuSongsDirCmd { get; }
    }
}