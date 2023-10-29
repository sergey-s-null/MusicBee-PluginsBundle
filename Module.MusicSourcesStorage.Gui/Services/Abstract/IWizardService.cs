using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Services.Abstract;

public interface IWizardService
{
    /// <returns>
    /// Added music source or null if cancelled. 
    /// </returns>
    MusicSource? AddVkPostWithArchiveSource();

    /// <returns>
    /// Modified music source additional info or null if cancelled.
    /// </returns>
    MusicSourceAdditionalInfo? EditMusicSourceAdditionalInfo(int musicSourceId);
}