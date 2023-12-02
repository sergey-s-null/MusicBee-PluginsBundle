using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities.Abstract;

public interface IInitialMusicSourceAdditionalInfoContext
{
    MusicSourceAdditionalInfo? InitialAdditionalInfo { get; set; }
}