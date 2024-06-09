namespace Module.MusicSourcesStorage.Logic.Entities.EventArgs;

public sealed class CoverRemovedEventArgs : System.EventArgs
{
    public int FileId { get; }

    public CoverRemovedEventArgs(int fileId)
    {
        FileId = fileId;
    }
}