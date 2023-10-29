using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities.Abstract;

public interface IMusicSourceAdditionalInfoContext
{
    MusicSourceAdditionalInfo? AdditionalInfo { get; set; }
}