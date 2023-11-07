using Module.MusicSourcesStorage.Logic.Entities.Abstract;

namespace Module.MusicSourcesStorage.Logic.Entities;

public sealed class CompletedFileDownloadingTask : ITaskWithProgress<string>
{
    public event EventHandler<ProgressChangedEventArgs>? ProgressChanged;
    public event EventHandler<TaskFailedEventArgs>? Failed;
    public event EventHandler? Cancelled;
    public event EventHandler<TaskResultEventArgs<string>>? SuccessfullyCompleted;

    public Task<string> Task { get; }

    public CompletedFileDownloadingTask(string downloadedFilePath)
    {
        Task = System.Threading.Tasks.Task.FromResult(downloadedFilePath);
    }

    public void Activate()
    {
    }
}