using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities.Abstract;

public interface IEditMusicSourceAdditionalInfoContext
{
    MusicSourceAdditionalInfo? InitialAdditionalInfo { get; }
    MusicSourceAdditionalInfo? EditedAdditionalInfo { get; set; }
}