namespace Module.MusicSourcesStorage.Logic.Entities.EventArgs;

public class ProgressChangedEventArgs : System.EventArgs
{
    /// <summary>
    /// Downloading progress in range [0; 1].
    /// </summary>
    public double Progress { get; }

    public ProgressChangedEventArgs(double progress)
    {
        Progress = progress;
    }
}