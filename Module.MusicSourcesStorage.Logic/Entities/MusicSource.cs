namespace Module.MusicSourcesStorage.Logic.Entities;

public abstract class MusicSource
{
    public int Id { get; }
    public string Name { get; }
    public IReadOnlyList<SourceFile> Files { get; }

    protected MusicSource(
        int id,
        string name,
        IReadOnlyList<SourceFile> files)
    {
        Id = id;
        Name = name;
        Files = files;
    }
}