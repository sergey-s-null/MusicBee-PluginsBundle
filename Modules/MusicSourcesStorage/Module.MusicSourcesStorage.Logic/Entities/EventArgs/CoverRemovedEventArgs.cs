namespace Module.MusicSourcesStorage.Logic.Entities.EventArgs;

public sealed class CoverRemovedEventArgs : System.EventArgs
{
    public int SourceId { get; }
    public string DirectoryPath { get; }

    public CoverRemovedEventArgs(int sourceId, string directoryPath)
    {
        SourceId = sourceId;
        DirectoryPath = directoryPath;
    }
}