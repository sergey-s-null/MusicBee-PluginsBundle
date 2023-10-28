namespace Module.MusicSourcesStorage.Logic.Entities;

// todo move "Model" to Database level
public abstract class MusicSourceModel
{
    public int Id { get; }
    public string Name { get; }
    public IReadOnlyList<FileModel> Files { get; }

    protected MusicSourceModel(
        int id,
        string name,
        IReadOnlyList<FileModel> files)
    {
        Id = id;
        Name = name;
        Files = files;
    }
}