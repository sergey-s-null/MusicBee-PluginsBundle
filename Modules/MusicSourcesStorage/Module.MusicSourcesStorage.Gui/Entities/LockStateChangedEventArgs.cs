namespace Module.MusicSourcesStorage.Gui.Entities;

public sealed class LockStateChangedEventArgs : EventArgs
{
    public int FileId { get; }
    public bool IsLocked { get; }

    public LockStateChangedEventArgs(int fileId, bool isLocked)
    {
        FileId = fileId;
        IsLocked = isLocked;
    }
}