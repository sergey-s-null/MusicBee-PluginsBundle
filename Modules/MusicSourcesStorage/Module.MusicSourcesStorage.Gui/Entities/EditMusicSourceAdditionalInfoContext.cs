using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class EditMusicSourceAdditionalInfoContext :
    IMusicSourceContext,
    IInitialMusicSourceAdditionalInfoContext,
    IEditMusicSourceAdditionalInfoContext,
    IWizardErrorContext,
    IWizardResultContext<MusicSourceAdditionalInfo>
{
    public int MusicSourceId { get; }
    public MusicSourceAdditionalInfo? InitialAdditionalInfo { get; set; }
    public MusicSourceAdditionalInfo? EditedAdditionalInfo { get; set; }
    public string? Error { get; set; }
    public MusicSourceAdditionalInfo? Result { get; set; }

    public EditMusicSourceAdditionalInfoContext(int musicSourceId)
    {
        MusicSourceId = musicSourceId;
    }
}