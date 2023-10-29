using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public abstract class MusicSource
{
    public int Id { get; }
    public MusicSourceAdditionalInfo AdditionalInfo { get; }
    public MusicSourceType Type { get; }
    public IReadOnlyList<SourceFile> Files { get; }

    protected MusicSource(
        int id,
        MusicSourceAdditionalInfo additionalInfo,
        MusicSourceType type,
        IReadOnlyList<SourceFile> files)
    {
        Id = id;
        AdditionalInfo = additionalInfo;
        Type = type;
        Files = files;
        AdditionalInfo = additionalInfo;
    }
}