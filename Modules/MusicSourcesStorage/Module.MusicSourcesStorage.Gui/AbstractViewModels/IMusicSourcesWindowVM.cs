namespace Module.MusicSourcesStorage.Gui.AbstractViewModels;

public interface IMusicSourcesWindowVM
{
    IList<IMusicSourceVM> MusicSources { get; }
    IMusicSourceVM? SelectedMusicSource { get; set; }
}