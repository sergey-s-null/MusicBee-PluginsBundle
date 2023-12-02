using Module.MusicSourcesStorage.Gui.Entities.Abstract;
using Module.MusicSourcesStorage.Logic.Entities;

namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class AddingTorrentContext :
    ITorrentFileContext,
    IIndexedFilesContext,
    IInitialMusicSourceAdditionalInfoContext,
    IEditMusicSourceAdditionalInfoContext,
    IWizardErrorContext,
    IWizardResultContext<MusicSource>
{
    public string? TorrentFilePath { get; set; }
    public IReadOnlyList<SourceFile>? IndexedFiles { get; set; }
    public MusicSourceAdditionalInfo? InitialAdditionalInfo { get; set; }
    public MusicSourceAdditionalInfo? EditedAdditionalInfo { get; set; }

    public string? Error { get; set; }
    public MusicSource? Result { get; set; }
}