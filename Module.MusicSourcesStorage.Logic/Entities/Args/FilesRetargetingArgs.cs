namespace Module.MusicSourcesStorage.Logic.Entities.Args;

public sealed record FilesRetargetingArgs(
    int SourceId,
    MusicSourceAdditionalInfo PreviousAdditionalInfo,
    MusicSourceAdditionalInfo CurrentAdditionalInfo
);