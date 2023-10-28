using Module.MusicSourcesStorage.Logic.Enums;

namespace Module.MusicSourcesStorage.Logic.Entities;

public abstract class SourceFile
{
    public int Id { get; }
    public string Path { get; }
    public long Size { get; }
    // todo del?
    public FileType Type { get; }

    protected SourceFile(int id, string path, long size, FileType type)
    {
        Id = id;
        Path = path;
        Size = size;
        Type = type;
    }
}