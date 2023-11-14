namespace Module.MusicSourcesStorage.Logic.Entities.EventArgs;

public sealed class CoverChangedEventArgs : System.EventArgs
{
    public int SourceId { get; }

    public int ImageFileId { get; }
    public string ImageFileRelativePath { get; }
    public byte[] ImageData { get; }

    public CoverChangedEventArgs(int sourceId, int imageFileId, string imageFileRelativePath, byte[] imageData)
    {
        SourceId = sourceId;
        ImageFileId = imageFileId;
        ImageFileRelativePath = imageFileRelativePath;
        ImageData = imageData;
    }
}