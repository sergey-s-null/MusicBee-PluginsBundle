using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface IWizardService
{
    /// <returns>
    /// Added music source or null if cancelled. 
    /// </returns>
    MusicSource? AddVkPostWithArchiveSource();

    void EditMusicSourceAdditionalInfo(int musicSourceId);
}