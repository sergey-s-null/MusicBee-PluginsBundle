namespace Module.MusicSourcesStorage.Logic.Entities.EventArgs;

public sealed class ProcessingStateChangedEventArgs : System.EventArgs
{
    public bool IsProcessing { get; }

    public ProcessingStateChangedEventArgs(bool isProcessing)
    {
        IsProcessing = isProcessing;
    }
}