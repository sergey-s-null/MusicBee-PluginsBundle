using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public abstract class MusicSource
{
    public int Id { get; }
    public string Name { get; }
    public MusicSourceType Type { get; }
    public IReadOnlyList<SourceFile> Files { get; }

    protected MusicSource(
        int id,
        string name,
        MusicSourceType type,
        IReadOnlyList<SourceFile> files)
    {
        Id = id;
        Name = name;
        Type = type;
        Files = files;
    }
}