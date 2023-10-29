using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class EditMusicSourceAdditionalInfoContext :
    IMusicSourceContext,
    IMusicSourceAdditionalInfoContext,
    IWizardErrorContext,
    IWizardResultContext<MusicSourceAdditionalInfo>
{
    public int MusicSourceId { get; }
    public MusicSourceAdditionalInfo? AdditionalInfo { get; set; }
    public string? Error { get; set; }
    public MusicSourceAdditionalInfo? Result { get; set; }

    public EditMusicSourceAdditionalInfoContext(int musicSourceId)
    {
        MusicSourceId = musicSourceId;
    }
}